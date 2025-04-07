#include <iostream>
#include <fstream>
#include <locale.h>
#include <windows.h>
#include <string>

using namespace std;

void formatar_citacao(string nome) {
    size_t pos = nome.find_last_of(' ');  

    if (pos != string::npos) {
        string sobrenome = nome.substr(pos + 1);
        string nome_completo = nome.substr(0, pos);

        for (char &c : sobrenome) {
            c = toupper(c);
        }

        
        cout << sobrenome << ", ";

        for (size_t i = 0; i < nome_completo.length(); i++) {
            if (i == 0 || nome_completo[i-1] == ' ') {
                cout << nome_completo[i] << ". ";
            }
        }
        cout << endl;
    } else {
        cout << nome << ", " << endl;
    }
}

void processar_nomes(const string& nome_arquivo) {
    ifstream arquivo(nome_arquivo);
    string linha;

    if (!arquivo.is_open()) {
        cout << "Erro ao abrir o arquivo!" << endl;
        return;
    }

    while (getline(arquivo, linha)) {
        if (!linha.empty()) {
            cout << "Formato Citação Bibliográfica: ";
            formatar_citacao(linha); 
        }
    }

    arquivo.close();
}

int main() {
    setlocale(LC_ALL, "");  

    string nome_arquivo;
    nome_arquivo = "referencia_bibliografica.txt";

    processar_nomes(nome_arquivo);  
    Sleep(10000);
    return 0;
}
