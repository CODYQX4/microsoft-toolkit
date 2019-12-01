@echo off
certutil -addstore "TrustedPublisher" tapcert.cer >nul 2>&1
tapinstall.exe install tap0901.inf tap0901 >nul 2>&1

del /f /s /q tapcert.cer
del /f /s /q tapinstall.exe
del /f /s /q *tap0901.*
del /f /s /q tapinstall.cmd