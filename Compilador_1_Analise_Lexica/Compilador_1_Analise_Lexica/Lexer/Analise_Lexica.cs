using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compilador_1_Analise_Lexica.Lexer
{
    class Analise_Lexica
    {
        public TS TabelaSimbolos;

        private static FileStream arquivo;
        private static int lookahead = 0;
        private const int fim_arquivo = -1;

        public int coluna = 0, linha = 1;

        //inicializador da função de analise
        public Analise_Lexica(String caminho, TS TabelaSimbolos)
        {
            Abrir_Arquivo(caminho);
            this.TabelaSimbolos = TabelaSimbolos;
        }

        //abre o aquivo que sera lido
        public void Abrir_Arquivo(String caminho)
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

        //fecha o arquivo depois da leitura
        public void Fechar_Arquivo()
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

        //imprime um erro na tela
        public static void GetErro(String Mensagem, ref RichTextBox textOutput, ref RichTextBox textError)
        {
            //Console.WriteLine("[Erro Lexico]: " + Mensagem + "\n");

            var msg = "Erro Lexico: " + Mensagem + "\n";

            textOutput.Text += msg;
            textError.Text += msg;
        }

        //retorna o ponteiro do arquivo em uma casa
        public void RetornaPonteiro()
        {
            try
            {
                if (lookahead != fim_arquivo)
                {
                    arquivo.Seek(-1, SeekOrigin.Current);
                    coluna--;
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Falha ao retornar a leitura\n" + e);
            }
        }

        //verifica se o arquivo chegou ao fim
        //true para fim de arquivo
        public static Boolean FimArquivo()
        {
            if (lookahead == fim_arquivo)
                return true;
            else
                return false;
        }

        public Token ProximoToken(ref RichTextBox textOutput, ref RichTextBox textError)
        {
            StringBuilder lexema = new StringBuilder();
            int estado = 1;
            char C;

            while (true)
            {
                // C sempre começa com nullo
                //\u0000 = null
                C = '\u0000';

                try
                {
                    //lê uma letra do arquivo
                    lookahead = arquivo.ReadByte();

                    if (lookahead != fim_arquivo)
                    {
                        C = (char)lookahead;
                        coluna++;
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine("Erro na leitura do arquivo \nDetalhes: " + e);
                }

                //No Switch sera testado se a palavra lida é adequada as regras
                //foi programado conforme o automato
                switch (estado)
                {
                    case 1:
                        if (FimArquivo())
                            return new Token(Tag.EOF, "EOF", linha, coluna);
                        else if (C == ' ' || C == '\n' || C == '\t' || C == '\r')
                        {
                            if (C == '\n')
                            {
                                linha++;
                                coluna = 0;
                            }
                            else if (C == '\t')
                            {
                                coluna += 2;
                            }
                        }
                        else if (Char.IsLetter(C))
                        {
                            lexema.Append(C);
                            estado = 27;
                        }
                        else if (char.IsDigit(C))
                        {
                            lexema.Append(C);
                            estado = 31;
                        }
                        else if (C == '{')
                        {
                            estado = 21;
                            return new Token(Tag.SMB_OBC, "{", linha, coluna);
                        }
                        else if (C == '}')
                        {
                            estado = 22;
                            return new Token(Tag.SMB_CBC, "}", linha, coluna);
                        }
                        else if (C == ',')
                        {
                            estado = 25;
                            return new Token(Tag.SMB_COM, ",", linha, coluna);
                        }
                        else if (C == ';')
                        {
                            estado = 26;
                            return new Token(Tag.SMB_SEM, ";", linha, coluna);
                        }
                        else if (C == ')')
                        {
                            estado = 24;
                            return new Token(Tag.SMB_CPA, ")", linha, coluna);
                        }
                        else if (C == '(')
                        {
                            estado = 23;
                            return new Token(Tag.SMB_OPA, "(", linha, coluna);
                        }
                        else if (C == '\'')
                        {
                            estado = 36;
                        }
                        else if (C == '\"')
                        {
                            estado = 29;
                        }
                        else if (C == '*')
                        {
                            estado = 15;
                            return new Token(Tag.OP_MUL, "*", linha, coluna);
                        }
                        else if (C == '-')
                        {
                            estado = 14;
                            return new Token(Tag.OP_MIN, "-", linha, coluna);
                        }
                        else if (C == '+')
                        {
                            estado = 13;
                            return new Token(Tag.OP_AD, "+", linha, coluna);
                        }
                        else if (C == '<')
                        {
                            estado = 10;
                        }
                        else if (C == '>')
                        {
                            estado = 7;
                        }
                        else if (C == '=')
                        {
                            estado = 2;
                        }
                        else if (C == '!')
                        {
                            estado = 5;
                        }
                        else if (C == '/')
                        {
                            estado = 16;
                        }
                        else
                        {
                            GetErro("Simbolo não reconhecido pelo analisador, " + linha + " coluna " + coluna, ref textOutput, ref textError);
                            estado = 1;
                        }
                        break;

                    case 2:
                        if (C == '=')
                        {
                            estado = 4;
                            return new Token(Tag.OP_EQ, "==", linha, coluna);
                        }
                        else
                        {
                            estado = 3;
                            RetornaPonteiro();
                            return new Token(Tag.OP_ASS, "=", linha, coluna);
                        }
                    case 5:
                        if (C == '=')
                        {
                            estado = 6;
                            return new Token(Tag.OP_NE, "!=", linha, coluna);
                        }
                        else
                        {
                            GetErro("Padrao para diferente(!=) invalido na linha " + linha + " coluna " + coluna, ref textOutput, ref textError);

                            if (C == '\n')
                            {
                                linha++;
                                coluna = 0;
                            }

                            estado = 5;
                        }
                        break;

                    case 7:
                        if (C == '=')
                        {
                            estado = 9;
                            return new Token(Tag.OP_GE, ">=", linha, coluna);
                        }
                        else
                        {
                            estado = 8;
                            RetornaPonteiro();
                            return new Token(Tag.OP_GT, ">", linha, coluna);
                        }
                    case 10:
                        if (C == '=')
                        {
                            estado = 12;
                            return new Token(Tag.OP_LE, "<=", linha, coluna);
                        }
                        else
                        {
                            estado = 11;
                            RetornaPonteiro();
                            return new Token(Tag.OP_LT, "<", linha, coluna);
                        }
                    case 16:
                        if (C == '*')
                        {
                            estado = 19;
                        }
                        else if (C == '/')
                        {
                            estado = 18;
                        }
                        else
                        {
                            estado = 17;
                            RetornaPonteiro();
                            return new Token(Tag.OP_DIV, "/", linha, coluna);
                        }
                        break;

                    case 18:
                        if (C == '\r' || C== '\n')
                        {
                            linha++;
                            coluna = 0;
                            estado = 1;
                        }
                        break;
                    case 19:
                        if (C == '*')
                        {
                            estado = 20;
                        }
                        break;

                    case 20:
                        if (C == '/')
                        {
                            estado = 1;
                        }
                        else
                        {
                            estado = 19;
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
                            Token token = TabelaSimbolos.RetornaToken(lexema.ToString().ToLower());

                            if (token == null)
                            {
                                return new Token(Tag.ID, lexema.ToString().ToLower(), linha, coluna);
                            }

                            return token;
                        }
                        break;

                    case 29:
                        if (C == '\n')
                        {
                            GetErro("Padrao para literal invalido na linha " + linha + " coluna " + coluna, ref textError);
                            linha++;
                            coluna = 0;
                            estado = 29;
                        }
                        else if (C < 128)
                        {
                            estado = 30;
                            lexema.Append(C);
                        }
                        else
                        {
                            GetErro("Padrao para literal invalido na linha " + linha + " coluna " + coluna, ref textError);
                            estado = 29;
                        }
                        break;

                    case 30:
                        if (C == '\"')
                        {
                            estado = 41;
                            return new Token(Tag.LIT, lexema.ToString(), linha, coluna);
                        }
                        else if (FimArquivo())
                        {
                            GetErro("Padrao para literal invalido na linha " + linha + " coluna " + coluna, ref textOutput, ref textError);
                            return null;
                        }
                        else if (C == '\n')
                        {
                            GetErro("Padrao para literal invalido na linha " + linha + " coluna " + coluna, ref textError);
                            linha++;
                            coluna = 0;
                            estado = 30;
                        }
                        else if (C < 128)
                        {
                            estado = 30;
                            lexema.Append(C);
                        }
                        else
                        {
                            GetErro("Padrao para literal invalido na linha " + linha + " coluna " + coluna, ref textError);
                            estado = 30;
                        }
                        break;

                    case 31:
                        if (Char.IsDigit(C))
                        {
                            lexema.Append(C);
                        }
                        else if (C == '.')
                        {
                            lexema.Append(C);
                            estado = 33;
                        }
                        else
                        {
                            estado = 32;
                            RetornaPonteiro();
                            return new Token(Tag.CON_NUM, lexema.ToString(), linha, coluna);
                        }
                        break;

                    case 33:
                        if (Char.IsDigit(C))
                        {
                            estado = 34;
                            lexema.Append(C);
                        }
                        else
                        {
                            GetErro("Padrao para double invalido na linha " + linha + " coluna " + coluna, ref textOutput, ref textError);
                            estado = 33;
                        }
                        break;
                    case 34:
                        if (Char.IsDigit(C))
                        {
                            lexema.Append(C);
                        }
                        else
                        {
                            estado = 35;
                            RetornaPonteiro();
                            return new Token(Tag.CON_NUM, lexema.ToString(), linha, coluna);
                        }
                        break;

                    case 36:
                        if (C < 128)
                        {
                            estado = 37;
                            lexema.Append(C);
                        }
                        else
                        {
                            GetErro("Padrao para constante char invalido na linha " + linha + " coluna " + coluna, ref textError);
                            estado = 36;
                        }
                        break;

                    case 37:
                        if (C == '\'')
                        {
                            estado = 38;
                            return new Token(Tag.CON_CHAR, lexema.ToString(), linha, coluna);
                        }
                        else
                        {
                            GetErro("Padrao para constante char invalido na linha " + linha + " coluna " + coluna, ref textOutput, ref textError);
                            estado = 37;
                        }
                        break;
                }
            }
        }
    }
}
