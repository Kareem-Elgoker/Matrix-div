using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Sec
{
    internal class Program
    {
        
        static int integer_validation(string input)
        {
            int n; bool first = true;
            do
            {
                if (!first) Console.WriteLine("Invlid, try again......");
                Console.Write(input);
                first = false;
            } while (!int.TryParse(Console.ReadLine(), out n));
            return n;
        }

        static double double_validation(string input)
        {
            double n; bool first = true;
            do
            {
                if (!first) Console.WriteLine("Invlid, try again......");
                Console.Write(input);
                first = false;
            } while (!double.TryParse(Console.ReadLine(), out n));
            return n;
        }

        static void Main(string[] args)
        {
            
            Console.WriteLine("Note : to make a division for two matrices -> they should both be a n*n");

            int n = integer_validation("Enter n : ");

            double[,] matrix1 = new double[n, n];

            double[,] matrix2 = new double[n, n];
            double[,] matrix2_inverse = new double[n, n];

            double[,] division_answer = new double[n, n];

            Console.WriteLine();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrix1[i, j] = double_validation($"Matrix1[{i + 1}][{j + 1}] : ");
                }
            }

            Console.WriteLine();
            for (int i = 0; i < n; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    matrix2[i, j] = double_validation($"Matrix2[{i + 1}][{j + 1}] : ");
                    if (i == j) matrix2_inverse[i, j] = 1.0;
                }
            }

            for(int i = 0; i < n; i++)
            {
                double div = matrix2[i, i];
                if(div == 0.0)
                {
                    if(i == n - 1)
                    {
                        Console.WriteLine("\nThis divisor matrix2 is singular and can't have an inverse for it.\n");
                        return;
                    }

                    for (int j = 0; j < n; j++)
                    {
                        double temp = matrix2[i, j];
                        matrix2[i, j] = matrix2[i+1, j];
                        matrix2[i + 1, j] = temp;

                        double temp_inv = matrix2_inverse[i, j];
                        matrix2_inverse[i, j] = matrix2_inverse[i + 1, j];
                        matrix2_inverse[i + 1, j] = temp;
                    }
                    i--;
                    continue;
                }
                else
                {
                    for (int j = 0; j < n; j++)
                    {
                        matrix2_inverse[i, j] /= div;
                        matrix2[i, j] /= div;
                    }
                }
                
                for(int j = 0; j < n; j++)
                {
                    if (j == i) continue;

                    double multiplier = -1 * matrix2[j, i];
                    for(int k = 0; k < n; k++)
                    {
                        matrix2[j, k] += multiplier * matrix2[i, k];
                        matrix2_inverse[j, k] += multiplier * matrix2_inverse[i, k];
                    }
                }
                
            }

            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    for(int k = 0; k < n; k++)
                    {
                        division_answer[i, j] += matrix1[i, k] * matrix2_inverse[k, j];
                    }
                }
            }

            Console.WriteLine("\nMatrix1 / Matrix2 = ");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(division_answer[i, j] + " ");
                }
                Console.WriteLine();
            }

        }
    }
}