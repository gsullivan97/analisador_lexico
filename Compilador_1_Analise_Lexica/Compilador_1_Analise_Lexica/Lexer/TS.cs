using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador_1_Analise_Lexica.Lexer
{
    public class TS
    {
        //inicialização da tabela de simbolos
        private Dictionary<Token, Info_Identificador> TabelaSimbolos;

        public TS()
        {
            //Inicia a tabela de simbolos com todas as KW defindas pelo professor
            TabelaSimbolos = new Dictionary<Token, Info_Identificador>();

            Token palavra;

            palavra = new Token(Tag.KW, "program", 0, 0);
            this.TabelaSimbolos.Add(palavra, new Info_Identificador());


            palavra = new Token(Tag.KW, "if", 0, 0);
            this.TabelaSimbolos.Add(palavra, new Info_Identificador());


            palavra = new Token(Tag.KW, "else", 0, 0);
            this.TabelaSimbolos.Add(palavra, new Info_Identificador());

            palavra = new Token(Tag.KW, "while", 0, 0);
            this.TabelaSimbolos.Add(palavra, new Info_Identificador());

            palavra = new Token(Tag.KW, "write", 0, 0);
            this.TabelaSimbolos.Add(palavra, new Info_Identificador());

            palavra = new Token(Tag.KW, "read", 0, 0);
            this.TabelaSimbolos.Add(palavra, new Info_Identificador());

            palavra = new Token(Tag.KW, "num", 0, 0);
            this.TabelaSimbolos.Add(palavra, new Info_Identificador());

            palavra = new Token(Tag.KW, "char", 0, 0);
            this.TabelaSimbolos.Add(palavra, new Info_Identificador());

            palavra = new Token(Tag.KW, "not", 0, 0);
            this.TabelaSimbolos.Add(palavra, new Info_Identificador());

            palavra = new Token(Tag.KW, "or", 0, 0);
            this.TabelaSimbolos.Add(palavra, new Info_Identificador());

            palavra = new Token(Tag.KW, "and", 0, 0);
            this.TabelaSimbolos.Add(palavra, new Info_Identificador());
        }

        //Função que vai verifica se o token que foi lido pelo sistema existe ou não na tabela de simbolos
        public Token RetornaToken(String lexema)
        {
            foreach (Token token in TabelaSimbolos.Keys)
            {
                if (token.Lexema.Equals(lexema))
                {
                    return token;
                }
            }

            //caso nao ache o ID na tabela de simbolos add ele na mesma
            Token palavra = new Token(Tag.ID, lexema, 0, 0);
            this.TabelaSimbolos.Add(palavra, new Info_Identificador());

            return null;
        }

        //Formatação da saida da tabela de simbolos feita pelo professor
        public String toString()
        {
            String saida = "";
            int i = 1;
            foreach (Token token in TabelaSimbolos.Keys)
            {
                saida += ("posicao " + i + ": \t" + token.toString()) + "\n";
                i++;
            }
            return saida;
        }
    }
}
