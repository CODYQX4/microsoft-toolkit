using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Win32.TaskScheduler;

namespace Common
{
    /// <summary>
    /// Group of Utility Methods for Scheduled Tasks
    /// </summary>
    public static class Tasks
    {
        /// <summary>
        /// Create a new Scheduled Task
        /// </summary>
        /// <param name="installPath">Directory where the program to schedule resides</param>
        /// <param name="fileName">Name of the program file to schedule</param>
        /// <param name="taskName">Name of the Scheduled Task to create</param>
        /// <param name="taskFolder">Create Task in a Folder other than the Root Folder</param>
        /// <param name="triggers">List of Triggers to activate the task. If null, will add a Daily Trigger </param>
        public static void CreateTask(string installPath, string fileName, string taskName, string taskFolder = null, IEnumerable<Trigger> triggers = null)
        {
            using (TaskService ts = new TaskService())
            {
                // Create a new task definition and assign properties
                TaskDefinition td = ts.NewTask();
                td.RegistrationInfo.Description = taskName;

                // Allow Run On Battery Power
                td.Settings.DisallowStartIfOnBatteries = false;
                td.Settings.StopIfGoingOnBatteries = false;

                // Set Privilege Level
                try
                {
                    td.Principal.RunLevel = TaskRunLevel.Highest;
                }
                catch (NotV1SupportedException)
                {
                    //Ignore, not supported on XP
                }

                // Create an action that will launch whenever the trigger fires
                td.Actions.Add(new ExecAction(installPath + Path.DirectorySeparatorChar + fileName));

                // Add Triggers
                td.Triggers.Clear();
                if (triggers != null)
                {
                    foreach (Trigger trigger in triggers)
                    {
                        td.Triggers.Add(trigger);
                    }
                }
                else
                {
                    // Add a Daily Trigger
                    DailyTrigger dt = new DailyTrigger();
                    td.Triggers.Add(dt);
                }
                
                // Register the task in the passed folder
                if (taskFolder != null)
                {
                    // This will throw a NotV1SupportedException on Windows XP or Older
                    ts.GetFolder(taskFolder).RegisterTaskDefinition(taskName, td, TaskCreation.CreateOrUpdate, "SYSTEM", null, TaskLogonType.ServiceAccount);
                }
                // Register the task in the root folder
                else
                {
                    ts.RootFolder.RegisterTaskDefinition(taskName, td, TaskCreation.CreateOrUpdate, "SYSTEM", null, TaskLogonType.ServiceAccount);
                }
            }
        }

        /// <summary>
        /// Delete a Scheduled Task
        /// </summary>
        /// <param name="taskName">Name of the Scheduled Task to delete</param>
        /// <param name="taskFolder">Folder where the Task to Delete Resides. If null, will delete all Tasks with the given name.</param>
        public static void DeleteTask(string taskName, string taskFolder = null)
        {
            if (IsTaskInstalled(taskName))
            {
                using (TaskService ts = new TaskService())
                {
                    // Delete From Specific Folder
                    if (taskFolder != null)
                    {
                        ts.GetFolder(taskFolder).DeleteTask(taskName);
                        return;
                    }

                    // Check if Task is In Root Folder
                    foreach (Task t in ts.RootFolder.Tasks)
                    {
                        if (t.Name == taskName)
                        {
                            ts.RootFolder.DeleteTask(taskName);
                        }
                    }

                    // Check All Subfolders
                    try
                    {
                        foreach (TaskFolder folder in ts.RootFolder.SubFolders)
                        {
                            foreach (Task t in folder.Tasks)
                            {
                                if (t.Name == taskName)
                                {
                                    folder.DeleteTask(taskName);
                                }
                            }
                        }
                    }
                    catch (NotV1SupportedException)
                    {
                        // Ignore, Subfolders Don't Exist on Windows XP
                    }
                }
            }
        }

        /// <summary>
        /// Check if a Scheduled Task Exists
        /// </summary>
        /// <param name="taskName">Name of the Scheduled Task to search for</param>
        /// <returns>True if the Task exists, False if it does not exist</returns>
        public static bool IsTaskInstalled(string taskName)
        {
            using (TaskService ts = new TaskService())
            {
                // Check if Task is In Root Folder
                foreach (Task t in ts.RootFolder.Tasks)
                {
                    if (t.Name == taskName)
                    {
                        return true;
                    }
                }

                // Check All Subfolders
                try
                {
                    foreach (TaskFolder folder in ts.RootFolder.SubFolders)
                    {
                        foreach (Task t in folder.Tasks)
                        {
                            if (t.Name == taskName)
                            {
                                return true;
                            }
                        }
                    }
                }
                catch (NotV1SupportedException)
                {
                    // Ignore, Subfolders Don't Exist on Windows XP
                }
                return false;
            }
        }

        /// <summary>
        /// Get the Last Run Time of a Scheduled Task
        /// </summary>
        /// <param name="taskName">Name of the Scheduled Task to search for</param>
        /// <returns>DateTime when the Program launched by the Task last ran</returns>
        public static DateTime GetTaskLastRunTime(string taskName)
        {
            using (TaskService ts = new TaskService())
            {
                // Check if Task is In Root Folder
                foreach (Task t in ts.RootFolder.Tasks)
                {
                    if (t.Name == taskName)
                    {
                        return t.LastRunTime;
                    }
                }

                // Check All Subfolders
                try
                {
                    foreach (TaskFolder folder in ts.RootFolder.SubFolders)
                    {
                        foreach (Task t in folder.Tasks)
                        {
                            if (t.Name == taskName)
                            {
                                return t.LastRunTime;
                            }
                        }
                    }
                }
                catch (NotV1SupportedException)
                {
                    // Ignore, Subfolders Don't Exist on Windows XP
                }
                throw new Exception(taskName + " is not an installed Scheduled Task!");
            }
        }

        /// <summary>
        /// Get the Path to the Program launched by a Scheduled Task
        /// </summary>
        /// <param name="taskName">Name of the Scheduled Task to search for</param>
        /// <returns>Directory where the Program launched by the Task exists</returns>
        public static string GetTaskProgramPath(string taskName)
        {
            using (TaskService ts = new TaskService())
            {
                // Check if Task is In Root Folder
                foreach (Task t in ts.RootFolder.Tasks)
                {
                    if (t.Name == taskName)
                    {
                        return t.Definition.Actions.ToString();
                    }
                }

                // Check All Subfolders
                try
                {
                    foreach (TaskFolder folder in ts.RootFolder.SubFolders)
                    {
                        foreach (Task t in folder.Tasks)
                        {
                            if (t.Name == taskName)
                            {
                                return t.Definition.Actions.ToString();
                            }
                        }
                    }
                }
                catch (NotV1SupportedException)
                {
                    // Ignore, Subfolders Don't Exist on Windows XP
                }
                throw new Exception(taskName + " is not an installed Scheduled Task!");
            }           
        }
    }
}