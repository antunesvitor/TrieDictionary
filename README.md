Requisitos

> dotnet 3.0.100

Para executar, abra o terminal cmd ou shell e execute

dotnet run --\[comando\] "\[palavra\]" --idioma "[pt | en]" --limite \[1+\]

comando:
    --completar : trás uma lista de x palavras que completam o que foi inserido como "palavra", onde x é definido em --limite  
    --sugerir : trás uma lista de x palavras parecidas com "palavra", onde x é definido em --limite  
    --corrigir : trás a correção de  "palavra"  