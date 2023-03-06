using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Exam
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] arrLock = GetUser2DArr();
            int[,] arrKey = GetUser2DArr();

            string resultMessage = GetKeyToLockResult(arrLock, arrKey);

            Console.WriteLine(resultMessage);
            Console.ReadKey();
        }

        public static string GetKeyToLockResult(int[,] arrLock, int[,] arrKey)
        {
            int countKeys = 0;
            int countLocks = 0;
            int countMatch = 0;

            int[] firstKeyCoordinates = null;
            int[] firstLockCoordinates = null;
            int[] lastKeyCoordinates = null;
            int[] lastLockCoordinates = null;

            for (int i = 0; i < arrLock.GetLength(0); i++)
            {
                for (int j = 0; j < arrLock.GetLength(1); j++)
                {
                    if (arrKey[i, j] == 1)
                    {
                        countKeys += 1;
                        lastKeyCoordinates = new int[] {i, j};
                        if (arrKey[i, j] == 1 && arrLock[i, j] == 0)
                        {
                            countMatch += 1;
                        }

                        if (firstKeyCoordinates is null)
                        {
                            firstKeyCoordinates = new int[] {i, j};
                        }
                    }

                    if (arrLock[i, j] == 0)
                    {
                        countLocks += 1;
                        lastLockCoordinates = new int[] {i, j};
                        if (firstLockCoordinates is null)
                        {
                            firstLockCoordinates = new int[] {i, j};
                        }
                    }
                }
            }

            if (countKeys != countLocks)
            {
                return $"The key does not fit into the lock.";
            }

            if (countKeys == countMatch)
            {
                return $"The key falls into the lock.";
            }

            string keyNotFitMessage = "The key needs to be moved:";

            int countFirsStepY = countNeededOffset(firstLockCoordinates[0], firstKeyCoordinates[0]);
            int countFirstStepX = countNeededOffset(firstLockCoordinates[1], firstKeyCoordinates[1]);

            int countLastStepY = countNeededOffset(lastLockCoordinates[0], lastKeyCoordinates[0]);
            int countLastStepX = countNeededOffset(lastLockCoordinates[1], lastKeyCoordinates[1]);

            if (countFirsStepY != countLastStepY || countFirstStepX != countLastStepX)
            {
                return $"The key does not fit into the lock.";
            }

            if (countFirsStepY == countLastStepY && countFirsStepY != 0 && countLastStepY != 0)
            {
                keyNotFitMessage += $"\nMove {formatNumStepsOrStep(countFirsStepY)} to the {offsetYDirection(countFirsStepY)}.";
            }

            if (countFirstStepX == countLastStepX && countFirstStepX != 0 && countLastStepX != 0)
            {
                keyNotFitMessage += $"\nMove {formatNumStepsOrStep(countFirstStepX)} to the {offsetXDirection(countFirstStepX)}.";
            }
            return keyNotFitMessage;
        }

        public static string offsetYDirection(int stepY)
        {
            return stepY > 0 ? "up" : "down";
        }

        public static string offsetXDirection(int stepX)
        {
            return stepX > 0 ? "left" : "right";
        }

        public static int countNeededOffset(int lockNum, int keyNum)
        {
            if (lockNum < keyNum)
            {
                return keyNum - lockNum;
            }
            else
            {
                return -(lockNum - keyNum);
            }
        }

        public static string formatNumStepsOrStep(int stepNum)
        {
            int absStepNum = Math.Abs(stepNum);
            return absStepNum > 1 ? absStepNum + " steps" : absStepNum + " step";
        }

        public static int[,] InputArr(int n, int m)
        {
            int[,] arr = new int[n, m];
            for (int i = 0; i < n; i++)
            {
                string[] arrString = Console.ReadLine().Trim().Split();

                for (int j = 0; j < m; j++)
                {
                    arr[i, j] = int.Parse(arrString[j]);
                }
            }
            return arr;
        }

        public static int[,] GetUser2DArr()
        {
            string[] nm = Console.ReadLine().Trim().Split();

            int n = int.Parse(nm[0]);
            int m = int.Parse(nm[1]);

            return InputArr(n, m);
        }
    }
}
