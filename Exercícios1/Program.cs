namespace CSharp;

class Calculadora{
    
    public static float calcularArea(float baze, float altura)
    {
        return baze * altura;
    }
}

class Program
{
    static void Main(string[] args)
    {
        
        Console.WriteLine("--> Calcular a área de um Retângulo <--");
        Console.Write("Digite a base do retângulo: ");
        float baze = Convert.ToSingle(Console.ReadLine());
        Console.Write("Digite a altura do retângulo: ");
        float altura = Convert.ToSingle(Console.ReadLine());

        float area = Calculadora.calcularArea(baze, altura);
        
        Console.Write("A área do retângulo é: " + area);


        /*
        double dolar = 5.17;
        double euro = 6.14;
        double PesoArgentino = 0.05;

        Console.WriteLine("\n--> Conversão Monetária <--");
        Console.Write("Informe o valor em Real: R$");
        double real = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("U$" + (real / dolar));
        Console.WriteLine("£" + (real / euro));
        Console.WriteLine("$" + (real / PesoArgentino));

        Console.WriteLine("\n--> Qual o valor maior <--");
        Console.Write("Informe um valor: ");
        int valor1 = Convert.ToInt32(Console.ReadLine());
        Console.Write("Informe outro valor: ");
        int valor2 = Convert.ToInt32(Console.ReadLine());

        int maior; 
        int menor;
        if(valor1 == valor2){
            Console.WriteLine("Valores iguais!");
            maior = valor1;
            menor = valor2;
        }
        else if(valor1 > valor2){
            maior = valor1;
            menor = valor2;
        }else{
            maior = valor2;
            menor = valor1;
        }

        Console.WriteLine("\n--> Julgando sua idade <--");
        Console.Write("Informe a sua idade: ");
        int idade = Convert.ToInt32(Console.ReadLine());
        if(idade <= 13){
            Console.WriteLine("Criança(o)!");
        }else if(idade > 13 && idade <= 18){
            Console.WriteLine("Adolescente(o)!");
        }else if(idade > 18 && idade <= 60){
            Console.WriteLine("Adulto(a)!");
        }else{
            Console.WriteLine("Idoso(a)!");
        }
        */

        Console.WriteLine("\n--> Sequência Fibonacci <--");
        int pos;
        int fi , f1 = 1, f2 = 2;
        Console.WriteLine("Ver até qual posição da Sequência Fibonacci: ");
        Console.Write("Informe até qual sequência: ");
        pos = Convert.ToInt32(Console.ReadLine());
        Console.Write("0, 1, ");
        for(int i = 0; i < pos-2; i++){
            fi = f1;
            Console.Write(fi + ", ");
            fi = f1 + f2;
            f1 = f2;
            f2 = fi;
        }
    }
}
