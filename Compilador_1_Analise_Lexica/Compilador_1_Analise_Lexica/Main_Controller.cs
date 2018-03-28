using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compilador_1_Analise_Lexica.Lexer;
using Compilador_1_Analise_Lexica.Forms;
using System.Windows.Forms;

namespace Compilador_1_Analise_Lexica
{
    class Main_Controller
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());

            ////instancia tabela de simbolos
            //TS TabelaSimbolos = new TS();
            ////anlise lexica é onde a magica acontece, o caminho do arquivo e a tabela de simbolos devem ser passadas para que funcione
            //Analise_Lexica analise = new Analise_Lexica("C:\\Users\\samuel.rizzon\\Desktop\\Facul\\1° Semestre - 2018\\Compiladores\\TIG_unibh-analisador_lexico-cfb9eaf1d0bf\\Relatorio\\Codigos\\Teste1.txt", TabelaSimbolos);
            //Token token;

            //do
            //{
            //    //chamda parao proximo token que sera lido
            //    token = analise.ProximoToken();

            //    if (token != null)
            //    {
            //        //imprime o token com linas e colunas
            //        Console.WriteLine("Token: " + token.toString() + "\t Linha: " + analise.linha + "\t Coluna: " + analise.coluna);
            //    }

            //} while (token != null && token.Classe != Tag.EOF); // do while só para se chegar no fim do arquivo

            ////fecha o arquivo depos de ter terminado a analise
            //analise.Fechar_Arquivo();

            ////Imprimir a tabela de simbolos
            //Console.WriteLine("");
            //Console.WriteLine("Tabela de simbolos:");
            //Console.WriteLine(TabelaSimbolos.toString());

            //Console.ReadKey();
        }
    }
}
