using System.Linq;
using System.Text.RegularExpressions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class strCalc {

    public static double Calculate(string expression) {
        expression = ReplaceInExpression(expression);
        while(!IsCorrectExpression(expression)){
            Console.WriteLine("Incorrect input. Try again: ");
            expression = Console.ReadLine();
        }
        string rplExpression = ReversePolandNotation(expression);
        var arr = rplExpression.ToArray();
        Stack<double> st = new Stack<double>();
        for(int i = 0; i < rplExpression.Length; ++i)
            {
                double num;
                bool isNum = double.TryParse(rplExpression[i].ToString(), out num);
                if (isNum)
                    st.Push(num);
                else
                {
                    double op2;
                    switch(rplExpression[i])
                    {
                        case '+':
                            st.Push(st.Pop() + st.Pop());
                            break;
                        case '*':
                            st.Push(st.Pop() * st.Pop());
                            break;
                        case '-':
                            op2 = st.Pop();
                            st.Push(st.Pop() - op2);
                            break;
                        case '/':
                            op2 = st.Pop();
                            if(op2==0)
                                throw new DivideByZeroException("Divide by zero is not allowed");
                            st.Push(st.Pop() / op2);                            
                            break;
                        default:
                            break;
                    }
                }
            }
        return st.Pop();
    }
    private static string ReplaceInExpression(string expression){
        expression = expression.Replace("--", "+");
        expression = expression.Replace("+-", "-");
        expression = expression.Replace(" ", string.Empty);   
        return expression;
    }
    private static bool IsCorrectExpression(string expression){
        int countOfOpenBrackets = 0;
        int countOfCloseBrackets = 0;
        for(int i =0; i < expression.Length; ++i){
            if(countOfCloseBrackets > countOfOpenBrackets){
                return false;
            }
            if(expression[i]==')'){
                countOfCloseBrackets++;
                continue;
            }
            if(expression[i]=='('){
                countOfOpenBrackets++;
                continue;
            }
            
            if(IsOperator(expression[i])){ 
                if( (i+1) < expression.Length && IsOperator(expression[i+1]))
                    return false;
            }
            if(!IsOperator(expression[i]) && !IsOperandus(expression[i]))
                return false;
        }
        if(countOfOpenBrackets!=countOfCloseBrackets)
            return false;
        return true;
    }
    private static string ReversePolandNotation(string expression){
        Stack<char> stack = new Stack<char>();
        String str = expression;     
        StringBuilder formula = new StringBuilder();
        for (int i = 0; i < str.Length; i++){
            char x=str[i];
            if (x == '(')
                stack.Push(x);
            else if (x == ')'){
                while(stack.Count>0 && stack.Peek() != '('){
                    formula.Append(" ");
                    formula.Append(stack.Pop());
                    formula.Append(" ");
                }
                stack.Pop();
            } else if (IsOperandus(x)){
                formula.Append(x);
                if(((i+1) < str.Length && !IsOperandus(str[i+1])) || i+1 == str.Length)
                    formula.Append(" ");
            } else if (IsOperator(x)) {
                while(stack.Count>0 && stack.Peek() != '(' && Priority(x)<=Priority(stack.Peek()) ) {
                    formula.Append(" ");
                    formula.Append(stack.Pop());
                    formula.Append(" ");
                }
                stack.Push(x);
            }
            else {
                char y= stack.Pop();
                if (y!='(') 
                    formula.Append(y);
            }
        }
        while (stack.Count>0) {
            formula.Append(stack.Pop());
        }
        return formula.ToString();
    }


    static bool IsOperator(char c){
        return (c=='-'|| c=='+' || c=='*' || c=='/');
    }
    static bool IsOperandus(char c){
        return (c>='0' && c<='9' || c=='.');
    }
    static int Priority(char c){
        switch (c){
            case '=':
                return 1;
            case '+':
                return 2;
            case '-':
                return 2;
            case '*':
                return 3;
            case '/':
                return 3;
            default:
                throw new ArgumentException();                                          
        }
    }
}
    
