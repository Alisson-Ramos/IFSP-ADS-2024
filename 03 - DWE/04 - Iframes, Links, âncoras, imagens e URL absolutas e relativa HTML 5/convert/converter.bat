@echo off

REM Executa o cwebp.exe para converter a.png para a.webp
cwebp.exe a.png -o a.webp

REM Verifica se a conversão foi bem-sucedida e exibe uma mensagem correspondente
if exist "a.webp" (
    echo a.png convertido para a.webp com sucesso!
) else (
    echo Falha ao converter a.png para WebP.
)

pause
