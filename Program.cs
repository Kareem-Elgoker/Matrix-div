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

        static bool get_inverse(double[,] Matrix, double[,]Matrix_inverse, int n)
        {
            for (int i = 0; i < n; i++)
            {
                double div = Matrix[i, i];
                if (div == 0.0)
                {
                    if (i == n - 1)
                    {
                        Console.WriteLine("\nThis divisor matrix2 is singular(its determinate = 0) and can't have an inverse for it.\n");
                        return false;
                    }

                    for (int j = 0; j < n; j++)
                    {
                        double temp = Matrix[i, j];
                        Matrix[i, j] = Matrix[i + 1, j];
                        Matrix[i + 1, j] = temp;

                        double temp_inv = Matrix_inverse[i, j];
                        Matrix_inverse[i, j] = Matrix_inverse[i + 1, j];
                        Matrix_inverse[i + 1, j] = temp;
                    }
                    i--;
                    continue;
                }
                else
                {
                    for (int j = 0; j < n; j++)
                    {
                        Matrix_inverse[i, j] /= div;
                        Matrix[i, j] /= div;
                    }
                }

                for (int j = 0; j < n; j++)
                {
                    if (j == i) continue;

                    double multiplier = -1 * Matrix[j, i];
                    for (int k = 0; k < n; k++)
                    {
                        Matrix[j, k] += multiplier * Matrix[i, k];
                        Matrix_inverse[j, k] += multiplier * Matrix_inverse[i, k];
                    }
                }

            }
            return true;
        }

        static void matrices_mult(double[,] m1, double[,] m2, double[,] ans, int n)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    for (int k = 0; k < n; k++)
                    {
                        ans[i, j] += m1[i, k] * m2[k, j];
                    }
                }
            }
        }

        static void print_matrix(double[,] m, int n)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(String.Format("{0:0.##}", m[i, j]) + "\t");
                }
                Console.WriteLine();
            }
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

            if(!get_inverse(matrix2, matrix2_inverse, n)) return;

            Console.WriteLine("\nMatrix1 / Matrix2 = Matrix1 * InverseOf(Matrix2)");
            Console.WriteLine("\n#InverseOf(Matrix2) : ");
            print_matrix(matrix2_inverse, n);

            matrices_mult(matrix1, matrix2_inverse, division_answer, n);

            Console.WriteLine("\nMatrix1 / Matrix2 = ");
            print_matrix(division_answer, n);
            Console.WriteLine();

        }
    }
}
