using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMarketAnalyzer
{
    static class Algorithms
    {
        /*****************************************************************************
         *  FUNCTION:       GaussianBlur
         *  Description:    See below
         *  Parameters:     See below
         *****************************************************************************/
        public static void GaussianBlur(ref List<Double> pData, Double pSigma, int pWindowSize = 5)
        {
            List<Double> return_data = GaussianBlur(pData, pSigma, pWindowSize);
            pData = return_data;
        }

        /*****************************************************************************
         *  FUNCTION:       GaussianBlur
         *  Description:    Applies a Gaussian filter to the input array and returns the
         *                  filtered values in the same array
         *  Parameters:     
         *      pData       - Input data to apply the filter to
         *      pSigma      - Standard deviation parameter of the Gaussian function 
         *      pWindowSize - Convolution window size, in number of elements (default = 5)
         *****************************************************************************/
        public static List<Double> GaussianBlur(List<Double> pData, Double pSigma, int pWindowSize = 5)
        {
            List<Double> return_data = new List<double>(pData);
            List<Double> g_window = new List<double>();
            Double g_value, g_norm;
            int i, j, pad;
            
            if(pWindowSize % 2 == 0)
                pWindowSize += 1;

            g_norm = 1 / (Math.Sqrt(2*Math.PI) * pSigma);

            //Approximate the Gaussian (kernel)
            for(i = 0; i < pWindowSize; i++)
            {
                g_value = i - (int)(pWindowSize / 2.0);
                g_window.Add(g_norm * Math.Exp(-g_value * g_value / (2 * pSigma * pSigma)));
            }

            //Pad to determine where to begin and end the convolution
            pad = (pWindowSize - 1)/2;

            //Perform the convolution
            for (i = pad; i < pData.Count - pad; i++)
            {
                return_data[i] = 0;
                for(j = 0; j < pWindowSize; j++)
                {
                    return_data[i] += pData[i - (int)(pWindowSize / 2.0) + j] * g_window[j];
                }
            }

            //Handle corners (
            for (i += 0; i < pData.Count + pad; i++)
            {
                return_data[i % pData.Count] = 0;
                for (j = 0; j < pWindowSize; j++)
                {
                    return_data[i % pData.Count] += pData[(i - (int)(pWindowSize / 2.0) + j) % pData.Count] * g_window[j];
                }
            }

            return return_data;
        }

        /*****************************************************************************
         *  FUNCTION:       MeanFilter
         *  Description:    Applies an averaging filter to the input data and returns as 
         *                  a new array. ie. each element is adjusted based on an average
         *                  of surrounding data points.
         *  Parameters:     
         *      pData       - the input data to apply the filter to
         *      pWindowSize - the number of elements on either side of each data point
         *                    to average for the purpose of smoothing
         *****************************************************************************/
        public static List<Double> MeanFilter(List<Double> pData, int pWindowSize = 2)
        {
            List<Double> return_data = new List<double>(pData);
            Double scale_factor;
            int i, j, pad, d_wind;

            //Pad to determine where to begin and end the convolution
            pad = pWindowSize;

            //Perform the convolution
            for (i = 0; i < pData.Count; i++)
            {
                //use a dynamic window size that automatically adjusts at the edges to use the available data
                d_wind = pWindowSize;
                
                if(i < pWindowSize)
                {
                    d_wind = i;
                }
                else if ((pData.Count - i - 1) < pWindowSize)
                {
                    d_wind = pData.Count - i - 1;
                }
                scale_factor = 1.0 / (double)(2 * d_wind);

                if (d_wind != 0)
                {
                    return_data[i % pData.Count] = 0;
                    for (j = 0; j < d_wind; j++)
                    {
                        return_data[i % pData.Count] += pData[i - d_wind + j] * scale_factor;
                    }
                    for (j = 0; j < d_wind; j++)
                    {
                        return_data[i % pData.Count] += pData[i + d_wind - j] * scale_factor;
                    }
                }
            }

            //Alternate handling of the edges of the pData array
            //for (i += 0; i < pData.Count + pad; i++)
            //{
            //    return_data[i % pData.Count] = 0;
            //    for (j = 0; j < pWindowSize; j++)
            //    {
            //        return_data[i % pData.Count] += pData[(i - pWindowSize + j) % pData.Count] * scale_factor;
            //    }
            //    for (j = 0; j < pWindowSize; j++)
            //    {
            //        return_data[i % pData.Count] += pData[(i + pWindowSize - j) % pData.Count] * scale_factor;
            //    }
            //}

            return return_data;
        }

        /*****************************************************************************
         *  FUNCTION:       Normalize
         *  Description:    
         *  Parameters:     
         *      pInput       - the input data
         *****************************************************************************/
        public static List<Double> Normalize(List<Double> pInput, double pCenter)
        {
            List<Double> return_values = new List<double>(pInput);
            double diff;

            if(pInput.Count > 0)
            {
                return_values[0] = pCenter;
                for (int i = 1; i < pInput.Count; i++)
                {
                    diff = pInput[i] - pInput[0];
                    return_values[i] = pCenter + (diff / pInput[0]);
                }
            }
            
            return return_values;
        }

        public static List<Double> IncrementalPercentChange(List<Double> pInput, double pCenter)
        {
            List<Double> return_values = new List<double>(pInput);
            double diff;

            if (pInput.Count > 0)
            {
                return_values[0] = pCenter;
                for (int i = 1; i < pInput.Count; i++)
                {
                    diff = pInput[i] - pInput[i-1];
                    return_values[i] = pCenter + (diff / pInput[i-1]);
                }
            }

            return return_values;
        }

        /*****************************************************************************
         *  FUNCTION:       k_means
         *  Description:    Classifies the data points passed in pPopulation, into one
         *                  of the classes defined by class prototypes contained in 
         *                  pPrototypes, using the k-means algorithm. Outputs class labels 
         *                  in pClassLabels as the index of pPrototypes corresponding to 
         *                  the chosen class.
         *  Parameters:     
         *      pPopulation - the input data [x,y]
         *      pPrototypes - the prototypes [x,y] of the classes to choose from
         *      pK          - 
         *      pMaxIterations - maximum number of iterations (for test, to help prevent infinite recurssion)
         *      pClassLabels   - the final classification of each point in pPopulation.
         *      
         *  pClassLabels must contain the same number of elements as pPopulation. 
         *****************************************************************************/
        public static int k_means(double[,] pPopulation, double[,] pPrototypes, int pK, int pMaxIterations, out int[] pClassLabels)
        {
            //Stop when all prototypes are within this distance of where they started from (not moving)
            const double STOP_DISTANCE = 0.0;

            int num_classes = 1;
            int nSize = pPopulation.GetUpperBound(0) + 1;
            int n, k, min_index, itteration_count;
            int[] num_each_class = new int[pPrototypes.GetUpperBound(0) + 1];

            double distance, min_dist;
            double x, y;
            double[,] classprototypes = new double[pK, 2];

            bool stop = true;
            
            pClassLabels = new int[nSize];

            distance = 0;
            min_dist = 0;
            min_index = 0;

            itteration_count = pMaxIterations;
           
            if (nSize > pK && pPopulation.GetLength(1) == 2 && pMaxIterations > 0)
            {
                do
                {
                    //for each point in the population to be classified
                    for (n = 0; n < nSize; n++)
                    {
                        //determine distance from each class prototype
                        for (k = 0; k < pK; k++)
                        {
                            x = Math.Abs(pPopulation[n, 0] - pPrototypes[k, 0]);
                            y = Math.Abs(pPopulation[n, 1] - pPrototypes[k, 1]);

                            distance = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
                            if (k == 0 || distance < min_dist)
                            {
                                min_dist = distance;
                                min_index = k;
                            }

                        }
                        //Assign class label based on minimum distance
                        pClassLabels[n] = min_index;

                        //Increment the number of points assigned to this particular class
                        num_each_class[min_index]++;

                        //Add the dimensions of the classified point to the prototypes array
                        classprototypes[min_index, 0] += pPopulation[n, 0];
                        classprototypes[min_index, 1] += pPopulation[n, 1];
                    }

                    //compute new class prototypes
                    stop = true;
                    for (k = 0; k < pK; k++)
                    {
                        //x, y co-ordinates of the new prototype
                        classprototypes[k, 0] = (classprototypes[k, 0] / (double)num_each_class[k]);
                        classprototypes[k, 1] = (classprototypes[k, 1] / (double)num_each_class[k]);

                        //calculate distance between current and previous prototype
                        x = Math.Abs(classprototypes[k, 0] - pPrototypes[k, 0]);
                        y = Math.Abs(classprototypes[k, 1] - pPrototypes[k, 1]);
                        distance = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));

                        //Set the new prototype
                        pPrototypes[k, 0] = classprototypes[k, 0];
                        pPrototypes[k, 1] = classprototypes[k, 1];

                        //Reset the temporary prototype prior to next itteration
                        classprototypes[k, 0] = 0;
                        classprototypes[k, 1] = 0;
                        num_each_class[k] = 0;

                        //if any prototype moved by more than the stop-distance, ensure re-classification continues
                        if (distance > STOP_DISTANCE)
                        {
                            stop = false;
                        }
                    }

                    itteration_count--;
                }
                while (itteration_count > 0 && stop == false);

                num_classes = (new HashSet<int>(pClassLabels)).Count();
            }

            return num_classes;
        }

        /*****************************************************************************
         *  FUNCTION:       fuzzy_c_means
         *  Description:    Classifies the data points passed in pPopulation, into one
         *                  or more classes using the fuzzy c-means clustering algorithm.
         *                  Starts with the class prototypes initially defined by pPrototypes and
         *                  pNumClassesInit, then re-classifies and merges as necessary.
         *                  
         *                  Note: pNumClassesInit must equal the first dimension of pPopulation.
         * 
         *  Parameters:     
         *      pPopulation - the input data [x,y]
         *      pPrototypes - the prototypes [x,y] of the classes to choose from
         *      pNumClassesInit - the initial number of classes. Must equal the first dimension of pPrototypes
         *      pFuzzifier      - a 'fuzziness' factor. A higher value results in lower (less defined) membership
         *                        values, and 'fuzzier' clusters.
         *      pMaxIterations  - maximum number of iterations (for test, to help prevent infinite recurssion).
         *                        Also allows finite limitation of processing time.
         *      pClassLabels    - the final classification of each point in pPopulation. Each element is an 
         *                        integer (beginning at 1) corresponding to the associated cluster.
         *****************************************************************************/
        public static int fuzzy_c_means(double[,] pPopulation, double[,] pPrototypes, int pNumClassesInit, int pFuzzifier, int pMaxIterations, out int[] pClassLabels)
        {
            //Stop when all prototypes are within 0.5% of where they started from (not moving)
            const double STOP_DISTANCE = 0.05;

            int num_classes = 1;
            int nSize = pPopulation.GetUpperBound(0) + 1;
            int n, k, c, mem_index, itteration_count;
            int[] num_each_class = new int[pPrototypes.GetUpperBound(0) + 1];

            double distance_k, distance_c;
            double x, y;
            double[,] classprototypes = new double[pNumClassesInit, 2];

            bool stop = true;

            //fuzzy additions
            double fMem, max_fMem;
            double[] MemSum = new double[pNumClassesInit];
            double[,] n_Membership = new double[nSize, pNumClassesInit];

            pClassLabels = new int[nSize];

            distance_k = 0;
            distance_c = 0;
            max_fMem = 0;
            mem_index = 0;

            itteration_count = pMaxIterations;

            if (nSize > pNumClassesInit && pPopulation.GetLength(1) == 2 && pMaxIterations > 0)
            {
                do
                {
                    stop = true;

                    //Fuzzification
                    for (n = 0; n < nSize; n++)
                    {
                        for (k = 0; k < pNumClassesInit; k++)
                        {
                            //Initial membership of point n in class k
                            if(itteration_count == pMaxIterations)
                            {
                                n_Membership[n, k] = 0;
                            }
                            
                            //Compute the Euclidean distance of point n from class k
                            x = Math.Abs(pPopulation[n, 0] - pPrototypes[k, 0]);
                            y = Math.Abs(pPopulation[n, 1] - pPrototypes[k, 1]);
                            distance_k = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
                            
                            fMem = 0;
                            distance_c = 0;

                            //compute the membership of point n in class k, as a measurement of the distance to the k-th class
                            // relative to the distance to the remaining classes
                            for(c = 0; c < pNumClassesInit; c++)
                            {
                                x = Math.Abs(pPopulation[n, 0] - pPrototypes[c, 0]);
                                y = Math.Abs(pPopulation[n, 1] - pPrototypes[c, 1]);
                                distance_c = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
                                fMem += Math.Pow((distance_k / distance_c), 2 / (pFuzzifier - 1));
                            }
                            fMem = 1 / fMem;

                            //Sum of weights
                            classprototypes[k, 0] += (pPopulation[n, 0] * Math.Pow(fMem, pFuzzifier));
                            classprototypes[k, 1] += (pPopulation[n, 1] * Math.Pow(fMem, pFuzzifier));
                            MemSum[k] += Math.Pow(fMem, pFuzzifier);

                            //Remember the class that point n has the highest membership in
                            if (k == 0 || fMem > max_fMem)
                            {
                                max_fMem = fMem;
                                mem_index = k;
                            }

                            //if membership has changed by more than the stop threshold, then ensure itteration
                            if (Math.Abs(fMem - n_Membership[n, k]) > STOP_DISTANCE)
                            {
                                stop = false;
                            }

                            n_Membership[n, k] = fMem;
                        }

                        //Assign point to the class of which it has the highest membership
                        pClassLabels[n] = mem_index;
                    }

                    //Compute the fuzzy centroids
                    for (k = 0; k < pNumClassesInit; k++)
                    {
                        //x, y co-ordinates of the new prototype
                        classprototypes[k, 0] = (classprototypes[k, 0] / MemSum[k]);
                        classprototypes[k, 1] = (classprototypes[k, 1] / MemSum[k]);

                        //Set the new prototype
                        pPrototypes[k, 0] = classprototypes[k, 0];
                        pPrototypes[k, 1] = classprototypes[k, 1];

                        //Reset the temporary prototype prior to next itteration
                        classprototypes[k, 0] = 0;
                        classprototypes[k, 1] = 0;
                        MemSum[k] = 0;
                    }

                    itteration_count--;
                }
                while (itteration_count > 0 && stop == false);

                //return the final number of identified clusters 
                num_classes = (new HashSet<int>(pClassLabels)).Count();
            }

            return num_classes;
        }

        /*****************************************************************************
         *  FUNCTION:       ShiftedPearsonProductCoefficient
         *  
         *  Description:    Calculates the Pearson Product Coefficient (PPC) for each 
         *                  Equity in pInputList based on it's HistoricalPrice list, against
         *                  every other Equity in pInputList, after shifting its HistoricalPrice
         *                  left by pShiftValue.
         *                  
         *                  The intent is to find a predictive correlation. 
         *                  ie. if one price-series tends to lag another by 1 trading day
         *                  (in terms of its trend), then shifting that series left by 1 and
         *                  calculating the PPC with the other series ought to yield a strong
         *                  PPC correlation. See Helpers.PearsonProductCoefficient() for the
         *                  normal PPC calculation.
         *                  
         *  Parameters:     
         *      pInputList  - the input data, consisting of multiple Equity instances
         *      pShiftValue - the number of HistoricalPriceDates to shift data by before
         *                    computing the PPC.
         *****************************************************************************/
        public static Double[,] ShiftedPearsonProductCoefficient(List<Equity> pInputList, int pShiftValue)
        {
            int N, cSize, i, j, k, count, key1, key2;
            Double coeff, numerator, denominator;
            Double sumSqRange1, sumSqRange2, meanRange1, meanRange2;
            Double[,] CorrelationCoefficients;
            List<Equity> temp_list;

            N = pInputList.Count();
            cSize = N * (N - 1) / 2;

            CorrelationCoefficients = new Double[cSize,2];

            if (N > 0 && pShiftValue >= 0 && pShiftValue < N)
            {
                count = 1;
                for (i = 0; i < N; i++)
                {
                    coeff = 0;
                    denominator = 0;
                    meanRange1 = 0;
                    meanRange2 = 0;

                    temp_list = new List<Equity>(pInputList);
                    temp_list[i].TrimDataLeft(pShiftValue);

                    for (j = 0; j < N; j++)
                    {
                        temp_list[j].TrimDataRight(pShiftValue);

                        if(j >= i)
                        {
                            key2 = 0;
                        }
                        else
                        {
                            key2 = 1;
                        }

                        if (pInputList[i].HistoricalPrice.Count() == pInputList[j].HistoricalPrice.Count())
                        {
                            sumSqRange1 = 0;
                            sumSqRange2 = 0;
                            numerator = 0;
                            meanRange1 = pInputList[i].avgPrice;
                            meanRange2 = pInputList[j].avgPrice;

                            for (k = 0; k < pInputList[j].HistoricalPrice.Count(); k++)
                            {
                                sumSqRange1 += Math.Pow(pInputList[i].HistoricalPrice[k], 2);
                                sumSqRange2 += Math.Pow(pInputList[j].HistoricalPrice[k], 2);
                                numerator += (pInputList[i].HistoricalPrice[k] * pInputList[j].HistoricalPrice[k]);
                            }
                            numerator -= (pInputList[j].HistoricalPrice.Count() * meanRange1 * meanRange2);
                            denominator = Math.Pow((sumSqRange1 - (pInputList[i].HistoricalPrice.Count() * Math.Pow(meanRange1, 2))), 0.5) *
                                Math.Pow((sumSqRange2 - (pInputList[j].HistoricalPrice.Count() * Math.Pow(meanRange2, 2))), 0.5);
                            coeff = numerator / denominator;
                        }

                        key1 = Helpers.getHash(i + 1, j + 1, pInputList.Count());
                        CorrelationCoefficients[key1, key2] = coeff;
                    }
                    temp_list.Clear();
                    count++;
                }
            }

            return CorrelationCoefficients;
        }
        

    }
}
