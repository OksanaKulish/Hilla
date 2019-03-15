using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace CypherHilla
{
    public class HillCipher 
    {
        public static int num = 26;
        public int Det(Matrix<double> M)
        {
            double A = M[0, 0] * (M[1, 1] * M[2, 2] - M[1, 2] * M[2, 1]) -
                       M[0, 1] * (M[1, 0] * M[2, 2] - M[1, 2] * M[2, 0]) +
                       M[0, 2] * (M[1, 0] * M[2, 1] - M[1, 1] * M[2, 0]);
            int AI = (int)A % num >= 0 ? (int)A % num : (int)A % num + num;
            for (int i = 0; i < num; i++)
            {
                if (AI * i % num == 1)
                {
                    return i;
                }
            }
            return -1;
        }
        public Matrix<double> ModMinorCofactor(Matrix<double> M, int A)
        {
            Matrix<double> resMat = DenseMatrix.Create(3, 3, 0.0);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int x = i == 0 ? 1 : 0, y = j == 0 ? 1 : 0, x1 = i == 2 ? 1 : 2, y1 = j == 2 ? 1 : 2;
                    double r = ((M[x, y] * M[x1, y1] - M[x, y1] * M[x1, y]) * Math.Pow(-1, i + j) * A) % num;
                    resMat[i, j] = r >= 0 ? r : r + num;
                }
            }
            return resMat;
        }
        
        public List<int> Decrypt(List<int> cipherText, List<int> key)
        {
            List<double> keyD = key.ConvertAll(x => (double)x);
            List<double> CD = cipherText.ConvertAll(x => (double)x);
            int m = Convert.ToInt32(Math.Sqrt((key.Count)));
            Matrix<double> keyMatrix = DenseMatrix.OfColumnMajor(m, (int)key.Count / m, keyD.AsEnumerable());
            Console.WriteLine(keyMatrix);            
            
             Matrix<double> PMatrix = DenseMatrix.OfColumnMajor(m, (int)cipherText.Count / m, CD.AsEnumerable());
            Console.WriteLine(PMatrix);
            List<int> finalRes = new List<int>();
            if (keyMatrix.ColumnCount == 3)
            {
                keyMatrix = ModMinorCofactor(keyMatrix.Transpose(), Det(keyMatrix));
            }            
            else
            {
                keyMatrix = keyMatrix.Inverse();
                Console.WriteLine(keyMatrix.ToString());
                Console.WriteLine(((int)keyMatrix[0, 0]).ToString() + ", " + ((int)keyMatrix[0, 0]).ToString());
            }
            
            if (Math.Abs((int)keyMatrix[0, 0]).ToString() != Math.Abs((double)keyMatrix[0, 0]).ToString())
            {
                throw new SystemException();
            }
            for (int i = 0; i < PMatrix.ColumnCount; i++)
            {
                List<double> Res = new List<double>();
                Res = ((((PMatrix.Column(i)).ToRowMatrix() * keyMatrix) % num).Enumerate().ToList());
                for (int j = 0; j < Res.Count; j++)
                {
                    int x = (int)Res[j] >= 0 ? (int)Res[j] : (int)Res[j] + num;
                    finalRes.Add(x);
                }
            }

            /*for (int i = 0; i < finalRes.Count; i++)
            {
                Console.WriteLine(finalRes[i].ToString());
            }*/
            return finalRes;
        }
        public void Encrypt_Mock(List<int> plainText_Mock, List<int> key_mock)
        {
            List<double> keyD = key_mock.ConvertAll(x => (double)x);
            if (keyD.Count % num != 0)
            {
                while(keyD.Count % num != 0)
                    keyD.Add(0);
            }
            List<double> PD = plainText_Mock.ConvertAll(x => (double)x);
            if (PD.Count % num != 0)
            {
                while (PD.Count % num != 0)
                    PD.Add(0);
            }
            int m = Convert.ToInt32(Math.Sqrt((3)));
            /*Matrix<double> keyMatrix = DenseMatrix.OfColumnMajor(3, (int)keyD.Count / m, keyD.AsEnumerable());
             Console.WriteLine(keyMatrix);

             Matrix<double> PMatrix = DenseMatrix.OfColumnMajor(3, (int)PD.Count / m, PD.AsEnumerable());
             Console.WriteLine(PMatrix);
             for (int i = 0; i < 3; i++)
             {
                 for (int j = 0; j < 2; j++)
                     Console.Write(j);
                 Console.WriteLine(i);
              } 
             foreach (int i in plainText_Mock)
                 Console.Write(i);*/
        }

        public List<int> Encrypt(List<int> plainText, List<int> key)
        {
            List<double> keyD = key.ConvertAll(x => (double)x);
         
            List<double> PD = plainText.ConvertAll(x => (double)x);
            int m = Convert.ToInt32(Math.Sqrt((key.Count)));
            Matrix<double> keyMatrix = DenseMatrix.OfColumnMajor(m, (int)key.Count / m, keyD.AsEnumerable());            
            //Console.WriteLine(keyMatrix);
            
            Matrix<double> PMatrix = DenseMatrix.OfColumnMajor(m, (int)plainText.Count / m, PD.AsEnumerable());
            //Console.WriteLine(PMatrix);
            
            List<int> finalRes = new List<int>();
            for (int i = 0; i < PMatrix.ColumnCount; i++)
            {
                List<double> Res = new List<double>();
                Res = ((((PMatrix.Column(i)).ToRowMatrix() * keyMatrix) % num).Enumerate().ToList());
               
                for (int j = 0; j < Res.Count; j++)
                {
                    finalRes.Add((int)Res[j]);
                }
            }
            return finalRes;
        }
    }
}