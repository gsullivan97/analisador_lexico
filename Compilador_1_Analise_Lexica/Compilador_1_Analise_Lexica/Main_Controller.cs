using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compilador_1_Analise_Lexica.Lexer;

namespace Compilador_1_Analise_Lexica
{
    class Main_Controller
    {
        /*private static TS TabelaSimbolos;

        private static FileStream arquivo;
        private static int lookahead = 0;
        private const int fim_arquivo = -1;

        public static int coluna = 1, linha = 1;

        public Main_Controller(String caminho)
        {
            Abrir_Arquivo(caminho);
        }

        private static void Abrir_Arquivo(String caminho)
        {
            try
            {
                arquivo = File.OpenRead(@caminho);
            }
            catch (IOException e)
            {
                Console.WriteLine("Erro de abertura do arquivo " + caminho + "\n" + e);
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro do programa ou falha da tabela de simbolos\n" + e);
            }

        }

        private static void Fechar_Arquivo()
        {
            try
            {
                arquivo.Close();
                arquivo.Dispose();
            }
            catch (IOException e)
            {
                Console.WriteLine("Erro ao fechar arquivo\n" + e);
            }
        }

        private static void GetErro(String Mensagem)
        {
            Console.WriteLine("[Erro Lexico]: " + Mensagem + "\n");
        }

        private static void RetornaPonteiro()
        {
            try
            {
                if(lookahead != fim_arquivo)
                {
                    arquivo.Seek(-1,SeekOrigin.Current);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Falha ao retornar a leitura\n" + e);
            }
        }

        private static Boolean FimArquivo()
        {
            if (lookahead == fim_arquivo)
                return true;
            else
                return false;
        }

        public Token ProximoToken()
        {
            StringBuilder lexema = new StringBuilder();
            int estado = 1;
            char C;

            while (true)
            {
                C = '\u0000';

                try
                {
                    lookahead = arquivo.ReadByte();

                    if (lookahead != fim_arquivo)
                    {
                        C = (char)lookahead;
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine("Erro na leitura do arquivo \nDetalhes: " + e);
                }

                switch (estado)
                {
                    case 1:
                        if (FimArquivo())
                            return new Token(Tag.EOF, "EOF", linha, coluna);
                        else if (C == ' ' || C == '\n' || C == '\t' || C == '\r')//REVISAR
                        {

                        }
                        else if (Char.IsLetter(C))
                        {
                            lexema.Append(C);
                            estado = 27;
                        }
                        break;
                    case 27:
                        if (Char.IsLetterOrDigit(C))
                        {
                            lexema.Append(C);
                        }
                        else
                        {
                            estado = 28;
                            RetornaPonteiro();
                            Token token = TabelaSimbolos.RetornaToken(lexema.ToString());

                            if (token == null)
                            {
                                return new Token(Tag.ID, lexema.ToString(), linha, coluna);
                            }

                            return token;
                        }
                        break;
                }
            }
        }

        static void Main(string[] args)
        {
            Main_Controller analise = new Main_Controller("C:\\Users\\Greg\\Desktop\\Codigos\\Teste1.txt");
            Token token;

            do
            {
                token = analise.ProximoToken();

                if (token != null)
                {
                    Console.WriteLine("Token: " + token.toString() + "\t Linha: " + linha + "\t Coluna: " + coluna);
                }

            } while (token != null && token.Classe != Tag.EOF);

            Fechar_Arquivo();
            Console.ReadKey();
        }*/
        
        static void Main(string[] args)
        {
            TS TabelaSimbolos = new TS();
            Analise_Lexica analise = new Analise_Lexica("C:\\Users\\Greg\\Desktop\\Codigos\\Teste1.txt", TabelaSimbolos);
            Token token;

            do
            {
                token = analise.ProximoToken();

                if (token != null)
                {
                    Console.WriteLine("Token: " + token.toString() + "\t Linha: " + analise.linha + "\t Coluna: " + analise.coluna);
                }

            } while (token != null && token.Classe != Tag.EOF);

            analise.Fechar_Arquivo();

            //// Imprimir a tabela de simbolos
            //Console.WriteLine("");
            //Console.WriteLine("Tabela de simbolos:");
            //Console.WriteLine(TabelaSimbolos.toString());

            Console.ReadKey();
        }
    }
}
