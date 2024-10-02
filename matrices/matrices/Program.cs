
internal class Program
{
    static void PrintMatrix(double[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write($"{matrix[i,j]:0.###}\t");
            }
            Console.WriteLine();
        }
    }

    static double? Determinant(double[,] matrix)
    {
        int n = matrix.GetLength(0);
        int m = matrix.GetLength(1);

        if (n != m)
            return null;

        if (n == 1)
            return matrix[0, 0];

        if (n == 2)
            return matrix[0,0] * matrix[1,1] - matrix[0,1] * matrix[1,0];

        double determin = 0;

        for(int col = 0; col < n; ++col)
        {
            double[,] minor = CreateMinor(matrix, 0, col);

            int sign = (col % 2 == 0) ? 1 : -1;
            double? minorDeterminant = Determinant(minor);

            determin += sign * matrix[0, col] * minorDeterminant.Value;
        }

        return determin;
    }


    static double[,] CreateMinor(double[,] matrix, int excludeRow, int excludeCol)
    {
        int n = matrix.GetLength(0);
        int m = matrix.GetLength(1);

        int size = n - 1;
        double[,] minor = new double[size, size];

        int mRow = 0, mCol = 0;

        for (int i = 0; i < n; ++i)
        {
            if (i == excludeRow)
                continue;

            mCol = 0;
            for (int j = 0; j < m; ++j)
            {
                if (j == excludeCol)
                    continue;

                minor[mRow, mCol] = matrix[i, j];
                mCol++;
            }
            mRow++;
        }

        return minor;
    }

    static double[,]? InverseMatrix(double[,] matrix)
    {
        int n = matrix.GetLength(0);
        int m = matrix.GetLength(1);

        if (n != m)
            return null;

        double? det = Determinant(matrix);
        if (!det.HasValue || Math.Abs(det.Value) < 0.001)
            return null;

        double detDouble = (double)det.Value;
        double[,] inverse = new double[n, n];

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                double[,] minor = CreateMinor(matrix, i, j);
                double? minorDet = Determinant(minor);

                if (!minorDet.HasValue)
                    return null;

                int sign = ((i + j) % 2 == 0) ? 1 : -1;

                inverse[j, i] = sign * (double)minorDet.Value / detDouble;
            }
        }

        return inverse;
    }

    static double[,] Transpose(double[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        double[,] transposed = new double[cols, rows];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                transposed[j, i] = matrix[i, j];
            }
        }

        return transposed;
    }

    static void PrintVector(double[] vector)
    {
        for (int i = 0; i < vector.Length; i++)
        {
            Console.WriteLine($"x[{i + 1}] = {vector[i]:0.###}");
        }
    }

    static double[]? SolveLinearSystem(double[,] A, double[] b)
    {
        int n = A.GetLength(0);
        int m = A.GetLength(1);

        if (n != m)
        {
            Console.WriteLine("Матрица коэффициентов должна быть квадратной для уникального решения.");
            return null;
        }

        if (b.Length != n)
        {
            Console.WriteLine("Размер вектора свободных членов не соответствует количеству уравнений.");
            return null;
        }

        // Создание расширенной матрицы [A | b]
        double[,] augmentedMatrix = new double[n, m + 1];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                augmentedMatrix[i, j] = A[i, j];
            }
            augmentedMatrix[i, m] = b[i];
        }

        // Применение метода Гаусса с частичным выбором главного элемента
        for (int i = 0; i < n; i++)
        {
            int maxRow = i;
            for (int k = i + 1; k < n; k++)
            {
                if (Math.Abs(augmentedMatrix[k, i]) > Math.Abs(augmentedMatrix[maxRow, i]))
                {
                    maxRow = k;
                }
            }

            // Если максимальный элемент равен нулю, система либо несовместна, либо имеет бесконечно много решений
            if (Math.Abs(augmentedMatrix[maxRow, i]) < 1e-10)
            {
               
                bool allZero = true;
                for (int j = 0; j < m; j++)
                {
                    if (Math.Abs(augmentedMatrix[i, j]) > 1e-10)
                    {
                        allZero = false;
                        break;
                    }
                }

                if (allZero && Math.Abs(augmentedMatrix[i, m]) > 1e-10)
                {
                    Console.WriteLine("Система несовместна (нет решений).");
                    return null;
                }

                Console.WriteLine("Система имеет бесконечным много решений.");
                return null;
            }

            for (int j = 0; j < m + 1; j++)
            {
                double temp = augmentedMatrix[i, j];
                augmentedMatrix[i, j] = augmentedMatrix[maxRow, j];
                augmentedMatrix[maxRow, j] = temp;
            }

            // Приведение к верхнетреугольному виду
            for (int k = i + 1; k < n; k++)
            {
                double factor = augmentedMatrix[k, i] / augmentedMatrix[i, i];
                for (int j = i; j < m + 1; j++)
                {
                    augmentedMatrix[k, j] -= factor * augmentedMatrix[i, j];
                }
            }
        }

        // Проверка на наличие свободных переменных
        for (int i = 0; i < n; i++)
        {
            bool allZero = true;
            for (int j = 0; j < m; j++)
            {
                if (Math.Abs(augmentedMatrix[i, j]) > 1e-10)
                {
                    allZero = false;
                    break;
                }
            }

            if (allZero && Math.Abs(augmentedMatrix[i, m]) > 1e-10)
            {
                Console.WriteLine("Система несовместна (нет решений).");
                return null;
            }
        }

        double[] solution = new double[m];
        for (int i = n - 1; i >= 0; i--)
        {
            double sum = augmentedMatrix[i, m];
            for (int j = i + 1; j < m; j++)
            {
                sum -= augmentedMatrix[i, j] * solution[j];
            }

            if (Math.Abs(augmentedMatrix[i, i]) < 1e-10)
            {
                Console.WriteLine("Система имеет бесконечно много решений.");
                return null;
            }

            solution[i] = sum / augmentedMatrix[i, i];
        }

        return solution;
    }

    static void Main()
    {
        int n, m;
        Random rnd = new Random();
        Console.Write("Введите кол-во строк первой матрицы: ");
        n = Convert.ToInt32(Console.ReadLine());
        Console.Write("Введите кол-во столбцов первой матрицы: ");
        m = Convert.ToInt32(Console.ReadLine());
        double[,] matriceOne = new double[n, m];

        int a, b;
        Console.Write("Введите кол-во строк второй матрицы: ");
        a = Convert.ToInt32(Console.ReadLine());
        Console.Write("Введите кол-во столбцов второй матрицы: ");
        b = Convert.ToInt32(Console.ReadLine());
        double[,] matriceTwo = new double[a, b];

        int choice = 0;
        Console.Write("Введите 1 чтобы заполнить матрицы самостоятельно, 2 чтобы заполнить матрицу случайными значениями: ");
        choice = Convert.ToInt32(Console.ReadLine());

        if(choice == 1)
        {
            Console.WriteLine("Введите значения для первой матрицы:");
            for (int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    Console.Write($"({i + 1},{j + 1}): ");
                    matriceOne[i, j] = Convert.ToDouble(Console.ReadLine());
                }
            }
            Console.WriteLine("Введите значения для второй матрицы:");
            for (int i = 0; i < a; i++)
            {
                for (int j = 0; j < b; j++)
                {
                    Console.Write($"({i + 1},{j + 1}): ");
                    matriceTwo[i, j] = Convert.ToDouble(Console.ReadLine());
                }
            }

        }

        else if(choice == 2)
        {
            Console.Write("Ввведите диапозон случайных чисел [a, b] (сначала a, потом b): ");
            int firstNum = Convert.ToInt32(Console.ReadLine());
            int secondNum = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    matriceOne[i, j] = Convert.ToInt32(rnd.Next(firstNum, secondNum));
                }
            }

            for (int i = 0; i < a; i++)
            {
                for (int j = 0; j < b; j++)
                {
                    matriceTwo[i, j] = Convert.ToInt32(rnd.Next(firstNum, secondNum));
                }
            }
        }
        Console.WriteLine("Первая матрица: ");
        PrintMatrix(matriceOne);


        Console.WriteLine("Вторая матрица: ");
        PrintMatrix(matriceTwo);

        //
        if(n == a && m == b)
        {
            double[,] matriceSum = new double[n, m];
            Console.WriteLine("\nСложим матрицы и получим новую: ");
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; ++j)
                {
                    matriceSum[i,j] = matriceOne[i,j] + matriceTwo[i,j];
                }
            }
            PrintMatrix(matriceSum);

        }
        else
            Console.WriteLine("Матрицы невозможно сложить из-за разницы в размерностях.");

        if(m == a)
        {
            double[,] matriceMul = new double[n, b];
            Console.WriteLine("\nПеремножим матрицы и получим новую: ");
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < b; ++j)
                {
                    matriceMul[i, j] = 0;
                    for(int k = 0; k < m; ++k)
                    {
                        matriceMul[i, j] += matriceOne[i, k] * matriceTwo[k, j];
                    }
                }
            }
            PrintMatrix(matriceMul);
        }
        else
            Console.WriteLine("Матрицы невозможно перемножить из-за несоответствия размерностей!");
        
        Console.WriteLine("\nНайдем определители для первой и второй матрицы: ");
        double? detOne = Determinant(matriceOne);
        double? detTwo = Determinant(matriceTwo);
        if (detOne.HasValue)
            Console.WriteLine($"Определитель первой матрицы: {detOne.Value}");
        else
            Console.WriteLine("Нельзя вычислить определитель первой матрицы, т.к. она не квадратная!");
        if (detTwo.HasValue)
            Console.WriteLine($"Определитель второй матрицы: {detTwo.Value}");
        else
            Console.WriteLine("Нельзя вычислить определитель второй матрицы, т.к. она не квадратная!");

        Console.WriteLine();
        if (detOne.HasValue && Math.Abs(detOne.Value) > 0.001)
        {
            double[,]? inverseOne = InverseMatrix(matriceOne);
            if (inverseOne != null)
            {
                Console.WriteLine("Обратная матрица первой матрицы: ");
                PrintMatrix(inverseOne);
            }
            else
                Console.WriteLine("Невозможно найти обратную матрицу для первой матрицы!");
        }
        else
            Console.WriteLine("Невозможно найти обратную матрицу для первой матрицы (определитель равен нулю или матрица не квадратная)");

        if (detTwo.HasValue && Math.Abs(detTwo.Value) > 0.001)
        {
            double[,]? inverseTwo = InverseMatrix(matriceTwo);
            if (inverseTwo != null)
            {
                Console.WriteLine("Обратная матрица второй матрицы: ");
                PrintMatrix(inverseTwo);
            }
            else
                Console.WriteLine("Невозможно найти обратную матрицу для второй матрицы!");
        }
        else
            Console.WriteLine("Невозможно найти обратную матрицу для второй матрицы (определитель равен нулю или матрица не квадратная)");

        Console.WriteLine("\nТранспонированная первая матрица:");
        double[,] transposedOne = Transpose(matriceOne);
        PrintMatrix(transposedOne);

        Console.WriteLine("\nТранспонированная вторая матрица:");
        double[,] transposedTwo = Transpose(matriceTwo);
        PrintMatrix(transposedTwo);

        double[] vectorOne = new double[m];
        Console.Write("Введите вектор свободных членов для первой матрицы: ");
        for(int i = 0; i < m; ++i)
        {
            vectorOne[i] = Convert.ToDouble(Console.ReadLine());
        }
        double[]? solutionOne = null;

        Console.WriteLine("\nРешение системы линейных уравнений A x = b:");
        solutionOne = SolveLinearSystem(matriceOne, vectorOne);
        if (solutionOne != null)
        {
            Console.WriteLine("Уникальное решение системы:");
            PrintVector(solutionOne);
        }
        else
        {
            Console.WriteLine("Система не имеет уникального решения.");
        }

        double[] vectorTwo = new double[b];
        Console.Write("Введите вектор свободных членов для второй матрицы: ");
        for (int i = 0; i < b; ++i)
        {
            vectorTwo[i] = Convert.ToDouble(Console.ReadLine());
        }
        double[]? solutionTwo = null;

        Console.WriteLine("\nРешение системы линейных уравнений A x = b:");
        solutionTwo = SolveLinearSystem(matriceTwo, vectorTwo);
        if (solutionTwo != null)
        {
            Console.WriteLine("Уникальное решение системы:");
            PrintVector(solutionTwo);
        }
        else
        {
            Console.WriteLine("Система не имеет уникального решения.");
        }
    }
}