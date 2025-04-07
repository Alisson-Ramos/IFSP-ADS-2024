#include <iostream>
#include <string>
#include <locale.h>
#include <windows.h>
#include <algorithm>

using namespace std;

bool eh_palindromo(string texto) {
    texto.erase(remove(texto.begin(), texto.end(), ' '), texto.end());

    transform(texto.begin(), texto.end(), texto.begin(), ::tolower);
    
    string reverso = texto;
    reverse(reverso.begin(), reverso.end());
    
    return texto == reverso;
}

int main() {
    setlocale(LC_ALL, "");  
    string frase;
    
    cout << "Digite uma frase para verificar se é palíndromo: ";
    getline(cin, frase); 
    
    if (eh_palindromo(frase)) {
        cout << "A frase é um palíndromo!" << endl;
    } else {
        cout << "A frase não é um palíndromo!" << endl;
    }
    
    Sleep(1000); 
    return 0;
}
