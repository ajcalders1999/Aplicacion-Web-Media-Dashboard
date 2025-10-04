@echo off
echo Limpiando proyecto Peliculas...

REM Buscar y eliminar carpetas bin, obj y .vs en toda la solución
for /d /r . %%d in (bin,obj,.vs) do (
    if exist "%%d" (
        echo Eliminando %%d...
        rmdir /s /q "%%d"
    )
)

echo Limpieza completada.
pause
