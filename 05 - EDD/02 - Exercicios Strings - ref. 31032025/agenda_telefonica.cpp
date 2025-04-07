#include <iostream>
#include <fstream>
#include <locale.h>
#include <windows.h>
#include <string>

using namespace std;


void formatar_agenda(string nome) {
    size_t pos = nome.find_last_of(' ');

    if (pos != string::npos) {
        string sobrenome = nome.substr(pos + 1);
        string nome_completo = nome.substr(0, pos);

        cout << sobrenome << ", " << nome_completo << endl;  
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
            cout << "Formato Agenda Telefônica: ";
            formatar_agenda(linha); 
        }
    }

    arquivo.close();
}

int main() {
    setlocale(LC_ALL, ""); 

    string nome_arquivo;
    nome_arquivo = "agenda_telefonica.txt";

    processar_nomes(nome_arquivo); 
    Sleep(10000);
    return 0;
}
