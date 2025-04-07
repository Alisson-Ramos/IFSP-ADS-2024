#include <iostream>
#include <locale.h>
#include <windows.h>
#include <string>

using namespace std;

void gotoxy(short x, short y) {
    COORD coord = {x, y};
    SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), coord);
}

void texto_em_queda(string texto) {
    int starty = 5;
    int startx = 5; 
    
    gotoxy((80 - texto.length()) / 2, starty); 
    
    for (int i = 0; i < texto.length(); i++) {
        
        for (int j = starty; j <= 20; j++) {
            gotoxy((80 - texto.length()) / 2 + i, j);
            cout << texto[i];
            Sleep(100);
            if (j < 20) {
                gotoxy((80 - texto.length()) / 2 + i, j);
                cout << " ";
            }
        }
    }
}

int main() {
    setlocale(LC_ALL, "");
    string frase;
    
    cout << "Digite uma frase: ";
    cin >> frase; 
    
    texto_em_queda(frase);
    
    return 0;
}
