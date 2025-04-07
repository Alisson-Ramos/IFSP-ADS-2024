#include <iostream>
#include <cstdlib>
#include <ctime>

using namespace std;

#define POLTRONAS_FILEIRA 40
#define FILEIRAS 15

int** gMatriz_poltronas;

void menu_inicial();
int** gerar_matriz_poltronas();
void calcular_faturamento();
void exibir_mapa_ocupacao();
void reservar_poltrona(int fileira, int poltrona);
void preencher_reservas_aleatorias(int n);

void exibir_titulo() {
    cout << "\n=========================================" << endl;
    cout << " SISTEMA DE RESERVA DE POLTRONAS " << endl;
    cout << " Criado por: Alisson Santos, Davi Coelho " << endl;
    cout << " Versao: 0.0.1 " << endl;
    cout << "=========================================\n" << endl;
}

int** gerar_matriz_poltronas() {
    int** matriz = new int*[FILEIRAS];
    for (int i = 0; i < FILEIRAS; i++) {
        matriz[i] = new int[POLTRONAS_FILEIRA];
        for (int j = 0; j < POLTRONAS_FILEIRA; j++) {
            matriz[i][j] = 1;
        }
    }
    return matriz;
}

void exibir_mapa_ocupacao() {
    cout << "\n===== MAPA DE OCUPACAO =====\n";
    for (int i = 0; i < FILEIRAS; i++) {
        for (int j = 0; j < POLTRONAS_FILEIRA; j++) {
            cout << (gMatriz_poltronas[i][j] == 1 ? ". " : "# ");
        }
        cout << endl;
    }
    menu_inicial();
}

void reservar_poltrona(int fileira, int poltrona) {
    if (gMatriz_poltronas[fileira - 1][poltrona - 1] == 0) {
        cout << "\nERRO: A poltrona selecionada ja esta ocupada. Escolha outra.\n";
        menu_inicial();
        return;
    }
    gMatriz_poltronas[fileira - 1][poltrona - 1] = 0;
    cout << "\nSUCESSO: Poltrona " << poltrona << " na fileira " << fileira << " reservada com exito!\n";
    menu_inicial();
}

void calcular_faturamento() {
    double faturamento = 0.0;
    int ocupadas = 0;
    for (int i = 0; i < FILEIRAS; i++) {
        for (int j = 0; j < POLTRONAS_FILEIRA; j++) {
            if (gMatriz_poltronas[i][j] == 0) {
                ocupadas++;
                faturamento += (i < 5) ? 50.0 : (i < 10) ? 30.0 : 15.0;
            }
        }
    }
    cout << "\n===== RELATORIO DE FATURAMENTO =====\n";
    cout << "Total arrecadado: R$ " << faturamento << "\n";
    cout << "Poltronas ocupadas: " << ocupadas << "\n";
    cout << "============================================\n";
    menu_inicial();
}

void preencher_reservas_aleatorias(int n) {
    srand(time(0));
    for (int i = 0; i < n; i++) {
        int fileira = rand() % FILEIRAS + 1;
        int poltrona = rand() % POLTRONAS_FILEIRA + 1;
        if (gMatriz_poltronas[fileira - 1][poltrona - 1] == 1) {
            gMatriz_poltronas[fileira - 1][poltrona - 1] = 0;
            cout << "Reserva aleatoria realizada na Fileira " << fileira << ", Poltrona " << poltrona << "\n";
        } else {
            cout << "Aviso: A poltrona " << poltrona << " na fileira " << fileira << " ja estava ocupada.\n";
        }
    }
    menu_inicial();
}

void menu_inicial() {
    int opcao = -1;
    do {
        cout << "\n===== MENU PRINCIPAL =====\n";
        cout << "1 - Reservar poltrona\n";
        cout << "2 - Visualizar mapa de ocupacao\n";
        cout << "3 - Consultar faturamento\n";
        cout << "0 - Sair\n";
        cout << "Escolha uma opcao: ";
        cin >> opcao;

        switch (opcao) {
            case 0:
                cout << "\nEncerrando o programa...\n";
                exit(0);
            case 1: {
                int fileira, poltrona;
                cout << "\nInforme a fileira (1 - " << FILEIRAS << "): ";
                cin >> fileira;
                cout << "Informe a poltrona (1 - " << POLTRONAS_FILEIRA << "): ";
                cin >> poltrona;

                if (fileira < 1 || fileira > FILEIRAS || poltrona < 1 || poltrona > POLTRONAS_FILEIRA) {
                    cout << "\nERRO: Fileira ou poltrona invalida.\n";
                    menu_inicial();
                }
                reservar_poltrona(fileira, poltrona);
                break;
            }
            case 2:
                exibir_mapa_ocupacao();
                break;              
            case 3:
                calcular_faturamento();
                break;
            default:
                cout << "\nERRO: Opcao invalida. Tente novamente.\n";
                break;
        }
    } while (opcao < 0 || opcao > 3);
}

int main() {
    exibir_titulo();
    gMatriz_poltronas = gerar_matriz_poltronas();
    menu_inicial();
    return 0;
}
