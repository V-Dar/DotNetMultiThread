using System.Diagnostics;
using System.Text;

int matrixRowCount = 11000;
int matrixColumnCount = 11000;
int countThread = 5;
int[,] matrix1 = CreateMatrix(matrixRowCount, matrixColumnCount);
int[,] matrix2 = CreateMatrix(matrixRowCount, matrixColumnCount);
int[,] resultMatrix = new int[matrixRowCount, matrixColumnCount];
List<Thread> threads = new List<Thread>();

Stopwatch stopwatch = Stopwatch.StartNew();
int tail = matrixRowCount % countThread;
int rowsPerThread = matrixRowCount / countThread;
for (int threadNumber = 0; threadNumber < countThread; threadNumber++)
{
    int startIndex = threadNumber * rowsPerThread;
    int endIndex;
    if (threadNumber == countThread - 1)
    {
        endIndex = matrixRowCount;
    }
    else
    {
        endIndex = (threadNumber + 1) * rowsPerThread;
    }
    Thread thread = new Thread(() =>
    {
        for (int i = startIndex; i < endIndex; i++)
        {
            for (int j = 0; j < matrixColumnCount; j++)
            {
                resultMatrix[i, j] = matrix1[i, j] + matrix2[i, j];
            }
        }
    });
    threads.Add(thread);
    thread.Start();
}
foreach (var item in threads)
    item.Join();
stopwatch.Stop();

//Console.WriteLine(MatrixToString(matrix1));
//Console.WriteLine(MatrixToString(matrix2));
//Console.WriteLine(MatrixToString(resultMatrix));
Console.WriteLine(stopwatch.ToString());


stopwatch.Restart();
for (int i = 0; i < matrixRowCount; i++)
{
    for(int j = 0;j < matrixColumnCount; j++)
        resultMatrix[i, j] = matrix1[i, j] + matrix2[i, j];
}
stopwatch.Stop();
Console.WriteLine(stopwatch.ToString());


int[,] CreateMatrix(int rows, int columns)
{
    Random random = new Random();
    int[,] matrix = new int[rows, columns];
    for (int i = 0; i < rows; i++)
        for (int j = 0; j < columns; j++)
            matrix[i, j] = random.Next(-9, 9);
    return matrix;
}

string MatrixToString(int[,] matrix)
{
    StringBuilder sb = new StringBuilder();
    int rows = matrix.GetLength(0);
    int cols = matrix.GetLength(1);
    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < cols; j++)
            sb.Append(matrix[i, j] + "\t");
        sb.Append("\n");
    }
    return sb.ToString();
}