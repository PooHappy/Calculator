using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bsy_계산기
{
    public partial class bsy_계산기 : Form
    {
        Stack<string> stack_num = new Stack<string>();  //숫자넣을 스택
        Stack<string> stack_oper = new Stack<string>(); //연산자 넣을 스택
        Stack<string> stack_tmp_num = new Stack<string>();  //임시 숫자 넣을 스택
        Stack<string> stack_tmp_oper = new Stack<string>(); //임시 연산자 넣을 스택
        double num1, num2;  
        string buff = "";   
        string output = "";
        char oper;
        double result = 1;
        public bsy_계산기()
        {
            InitializeComponent();
        }
        private void bt_oper_click(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            if (buff != "") //buff가 ""이 아니면 -> buff에 숫자가 입력되어있으면
            {
                stack_num.Push(Convert.ToString(buff));   //숫자 스택 Push
            }
            stack_oper.Push(btn.Text);  //연산자를 연산자 스택에 Push해준다.
            buff = "";    // 0
            output += btn.Text;   // 3  + 
            textBox1.Text = output; // 3 + 8
            oper = btn.Text[0];   // + 
        }
        private void bt_num_click(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            // 숫자 키 중 어느 것이 눌러졌는지를 저장
            buff += btn.Text;      //  3   ,  0 , 8
            output += btn.Text;   // 3   ,   3 +  ,    3 + 8
            textBox1.Text = output;  // 3   , 3 + ,    3 + 8
        }
        private void bt_clear_click(object sender, MouseEventArgs e)
        {
            textBox1.Text = "";
            num1 = 0;
            num2 = 0;
            buff = "";
            output = "";
            result = 0.0;
            while (stack_num.Count() != 0)  //숫자 스택이 비워질때까지
                stack_num.Pop();            //Pop해준다
            while (stack_oper.Count() != 0) //연산자 스택이 비워질때까지
                stack_oper.Pop();           //Pop해준다
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = output;
        }

        private void bt_clac_click(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            if (buff != "")     //'=' 입력 전에 숫자가 입력되어 있으면
            {
                stack_num.Push(Convert.ToString(buff)); //숫자 스택 Push
            }
            while (stack_num.Count != 0)    //숫자 스택이 비어질때 까지
            {
                stack_tmp_num.Push(stack_num.Pop());    //숫자 스택의 맨 위 값을 임시 숫자 스택에 Push
            }
            while (stack_oper.Count != 0)   //연산자 스택이 비어질때 까지
            {
                stack_tmp_oper.Push(stack_oper.Pop());   //연산자 스택의 맨 위 값을 임시 연산자 스택에 Push
            }
            while (stack_tmp_oper.Count != 0)   //임시 연산자 스택이 비어질때까지
            {
                if (stack_tmp_oper.Peek() == "*" || stack_tmp_oper.Peek() == "/" || stack_tmp_oper.Peek() == "%" || stack_tmp_oper.Peek() == "√" || stack_tmp_oper.Peek() == "^" || stack_tmp_oper.Peek() == "!")
                    //연산자가 +나 -가 아니면
                {
                    switch (stack_tmp_oper.Peek())  //임시 연산자에 따라서
                    {
                        case "*":   //*이면 
                            num1 = Convert.ToDouble(stack_tmp_num.Pop());   //임시 숫자 스택에서 Pop해서 num1에 저장
                            num2 = Convert.ToDouble(stack_tmp_num.Pop());   //임시 숫자 스택에서 Pop해서 num2에 저장
                            result = num1 * num2;   //result에 계산값 저장
                            stack_tmp_oper.Pop();       //임시 연산자를 Pop하여 제거
                            stack_tmp_num.Push(Convert.ToString(result));   //result를 임시 숫자 스택에 Push
                            break;
                        case "/":
                            num1 = Convert.ToDouble(stack_tmp_num.Pop());   //임시 숫자 스택에서 Pop해서 num1에 저장
                            num2 = Convert.ToDouble(stack_tmp_num.Pop());   //임시 숫자 스택에서 Pop해서 num2에 저장
                            if (num2 == 0)
                            {
                                textBox1.Text = "0으로 나눌수 없음";
                                return;
                            }
                            result = num1 / num2;   //result에 계산값 저장
                            stack_tmp_oper.Pop();       //임시 연산자를 Pop하여 제거
                            stack_tmp_num.Push(Convert.ToString(result));    //result를 임시 숫자 스택에 Push
                            break;
                        case "%":
                            num1 = Convert.ToDouble(stack_tmp_num.Pop());   //임시 숫자 스택에서 Pop해서 num1에 저장
                            num2 = Convert.ToDouble(stack_tmp_num.Pop());   //임시 숫자 스택에서 Pop해서 num2에 저장
                            result = num1 % num2;   //result에 계산값 저장
                            stack_tmp_oper.Pop();       //임시 연산자를 Pop하여 제거
                            stack_tmp_num.Push(Convert.ToString(result));   //result를 임시 숫자 스택에 Push
                            break;
                        case "√":
                            num1 = Convert.ToDouble(stack_tmp_num.Pop());   //임시 숫자 스택에서 Pop해서 num1에 저장
                            result = Math.Pow(num1, 0.5);   //Math.Pow함수를 사용하여 루트값 사용
                            stack_tmp_oper.Pop();   //임시 연산자를 Pop하여 제거
                            stack_tmp_num.Push(Convert.ToString(result));   //result를 임시 숫자 스택에 Push
                            break;
                        case "^":
                            num1 = Convert.ToDouble(stack_tmp_num.Pop());   //임시 숫자 스택에서 Pop해서 num1에 저장
                            num2 = Convert.ToDouble(stack_tmp_num.Pop());   //임시 숫자 스택에서 Pop해서 num2에 저장
                            result = Math.Pow(num1, num2);  //Math.Pow함수를 사용하여 num1^num2 저장
                            stack_tmp_oper.Pop();    //임시 연산자를 Pop하여 제거
                            stack_tmp_num.Push(Convert.ToString(result));   //result를 임시 숫자 스택에 Push
                            break;
                        case "!":
                            num1 = Convert.ToDouble(stack_tmp_num.Pop());   //임시 숫자 스택에서 Pop해서 num1에 저장
                            for (double i = num1; i > 0; i--)
                            {
                                result = result * i;    //팩토리얼 계산
                            }
                            stack_tmp_oper.Pop();   //임시 연산자를 Pop하여 제거
                            stack_tmp_num.Push(Convert.ToString(result));    //result를 임시 숫자 스택에 Push
                            break;
                    }
                }
                else
                {   //연산자가 +나 -면
                    stack_num.Push(stack_tmp_num.Pop());    //임시 숫자 스택의 맨 위 값을 숫자 스택에 Push
                    stack_oper.Push(stack_tmp_oper.Pop());  //임시 연산자 스택의 맨 위 값을 연산자 스택에 Push
                }
            }

            while (stack_num.Count != 0)    //임시 숫자 스택이 비어질때 까지
            {
                stack_tmp_num.Push(stack_num.Pop());    //숫자 스택의 맨 위 값을 임시 숫자 스택에 Push
            }
            while (stack_oper.Count != 0)   //임시 연산자 스택이 비어질때 까지
            {
                stack_tmp_oper.Push(stack_oper.Pop());   //연산자 스택의 맨 위 값을 임시 연산자 스택에 Push
            }

            while (stack_tmp_oper.Count != 0)   //임시 연산자 스택이 비어질때 까지
            {
                switch (stack_tmp_oper.Peek())  //임시 연산자 판별
                {
                    case "+":   //+이면
                        num1 = Convert.ToDouble(stack_tmp_num.Pop());   //임시 숫자 스택에서 Pop해서 num1에 저장
                        num2 = Convert.ToDouble(stack_tmp_num.Pop());   ///임시 숫자 스택에서 Pop해서 num2에 저장
                        stack_tmp_oper.Pop();   ///임시 연산자 스택에서 Pop하여 제거
                        result = num1 + num2;   //계산값 result저장
                        stack_tmp_num.Push(Convert.ToString(result));   //result값을 숫자 스택에 Push
                        break;
                    case "-":
                        num1 = Convert.ToDouble(stack_tmp_num.Pop());   ///임시 숫자 스택에서 Pop해서 num1에 저장
                        num2 = Convert.ToDouble(stack_tmp_num.Pop());  ///임시 숫자 스택에서 Pop해서 num2에 저장
                        stack_tmp_oper.Pop();   ///임시 연산자 스택에서 Pop하여 제거
                        result = num1 - num2;   //계산값 result저장  
                        stack_tmp_num.Push(Convert.ToString(result));   //result값을 /임시 숫자 스택에 Push
                        break;
                }
            }
            result = Convert.ToDouble(stack_tmp_num.Peek());    //결과값을 result에 저장
            stack_num.Push(stack_tmp_num.Pop());    //임시 숫자 스택에서 pop하여 숫자 스택에 push
            output += btn.Text + result.ToString();   //  =  결과값 
            textBox1.Text =  output;
            buff = "";
            result = 1;
        }
    }
}