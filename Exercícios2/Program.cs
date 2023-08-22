namespace Exercícios2;

class Program
{
    static void Main(string[] args)
    {
        double[] arrayNumbers = new double[5];
        String user;
        double soma = 0, media, mediana = 0, maxCount = 0, moda = 0, maisAparece = 0;


        for (int i = 0; i < arrayNumbers.Length; i++)
        {
            Console.Write("Informe o " + i+1 + "º valor: ");
            user = Console.ReadLine();
            arrayNumbers[i] = double.Parse(user);
            soma += arrayNumbers[i];
        }

        foreach (var numbers in arrayNumbers)
        {
            var count = 0;
            for (int i = 0; i < arrayNumbers.Length; i++)
            {
                if (numbers == arrayNumbers[i]) count++;
            }
            if (count > maxCount){
                maxCount = count;
                moda = numbers;
            }
            
        }

        Array.Sort(arrayNumbers);
        media = soma / 5;
        mediana = arrayNumbers[arrayNumbers.Length / 2];
        Console.WriteLine("Média: " + media);
        Console.WriteLine("Mediana: " + mediana);
        Console.WriteLine("Moda: " + moda);

        
    }
}
