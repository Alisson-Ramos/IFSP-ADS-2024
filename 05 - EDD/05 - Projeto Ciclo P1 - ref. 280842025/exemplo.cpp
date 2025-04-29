#include <iostream>
#include <fstream>
#include <locale.h>
#include <sstream>
#include <vector>
#include <cctype>

using namespace std;

int main()
{
    setlocale(LC_ALL, "");



        
    vector<string> partes;
    stringstream ss("alisson santos");
    string palavra;


    while (ss >> palavra)
        partes.push_back(palavra);

    if (partes.size() == 0)
       return -1;

    string sobrenome = partes.back();
    partes.pop_back(); 


    string referencia = "";


    for (char& c : sobrenome)
        referencia += toupper(c);

    referencia += ", ";


    if (!partes.empty()) {
        referencia += partes[0] + " ";
    }


    for (size_t i = 1; i < partes.size(); ++i) {
        referencia += toupper(partes[i][0]);
        referencia += ". ";
    }

    cout << referencia << endl;
        


    return 0;
}