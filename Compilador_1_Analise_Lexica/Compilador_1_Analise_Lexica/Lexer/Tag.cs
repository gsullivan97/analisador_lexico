using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador_1_Analise_Lexica.Lexer
{
    public enum Tag
    {
        //ENUM serve aqui para declara o tipo de token que foi recebido

        //FIM ARQUIVO
        EOF,

        //OPERADORES
        OP_EQ,
        OP_NE,
        OP_GT,
        OP_LT,
        OP_GE,
        OP_LE,
        OP_AD,
        OP_MIN,
        OP_MUL,
        OP_DIV,
        OP_ASS,

        //SIMBOLOS
        SMB_OBC,
        SMB_CBC,
        SMB_OPA,
        SMB_CPA,
        SMB_COM,
        SMB_SEM,

        //PALAVRA RESERVADA
        KW,

        //IDENTIFICADOR
        ID,

        //LITERAL
        LIT,

        //CONSTANTES
        CON_NUM,
        CON_CHAR,
    }
}
