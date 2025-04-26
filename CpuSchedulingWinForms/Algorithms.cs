using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CpuSchedulingWinForms
{
    public static class Algorithms
    {
        public static void fcfsAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);
            double[] arrivalTime = new double[np];
            double[] bp = new double[np];
            double[] wtp = new double[np];
            double[] tat = new double[np];
            double[] ct = new double[np];
            double twt = 0.0, ttat = 0.0, awt, atat, cpuUtilization, throughput;
            int num;

            DialogResult result = MessageBox.Show("First Come First Serve Scheduling ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                for (num = 0; num < np; num++)
                {
                    string arrivalInput = Microsoft.VisualBasic.Interaction.InputBox("Enter Arrival time: ",
                                                           "Arrival time for P" + (num + 1),
                                                           "",
                                                           -1, -1);
                    arrivalTime[num] = Convert.ToDouble(arrivalInput);

                    string burstInput = Microsoft.VisualBasic.Interaction.InputBox("Enter Burst time: ",
                                                           "Burst time for P" + (num + 1),
                                                           "",
                                                           -1, -1);
                    bp[num] = Convert.ToDouble(burstInput);
                }

                for (num = 0; num < np; num++)
                {
                    if (num == 0)
                    {
                        wtp[num] = 0;
                        ct[num] = arrivalTime[num] + bp[num];
                    }
                    else
                    {
                        if (ct[num - 1] < arrivalTime[num])
                        {
                            ct[num] = arrivalTime[num] + bp[num];
                        }
                        else
                        {
                            ct[num] = ct[num - 1] + bp[num];
                        }
                        wtp[num] = ct[num - 1] - arrivalTime[num];
                        if (wtp[num] < 0) wtp[num] = 0;
                    }

                    tat[num] = wtp[num] + bp[num];
                    twt += wtp[num];
                    ttat += tat[num];

                    MessageBox.Show($"Process P{num + 1}:\nArrival Time: {arrivalTime[num]} sec(s)\nCompletion Time: {ct[num]} sec(s)\nTurnaround Time: {tat[num]} sec(s)\nWait Time: {wtp[num]} sec(s)",
                                    "Process Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                awt = twt / np;
                atat = ttat / np;
                cpuUtilization = ((ct[np - 1] - arrivalTime[0]) / ct[np - 1]) * 100;
                throughput = np / (ct[np - 1] - arrivalTime[0]);

                MessageBox.Show($"Summary:\nAverage Wait Time: {awt} sec(s)\nAverage Turnaround Time: {atat} sec(s)\nCPU Utilization: {cpuUtilization}%\nThroughput: {throughput} processes/sec",
                                "FCFS Summary", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static void sjfAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);

            double[] bp = new double[np];
            double[] wtp = new double[np];
            double[] tat = new double[np];
            double[] ct = new double[np];
            double[] p = new double[np];
            double twt = 0.0, ttat = 0.0, awt, atat, cpuUtilization, throughput;
            int x, num;
            double temp = 0.0;
            bool found = false;

            DialogResult result = MessageBox.Show("Shortest Job First Scheduling", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                for (num = 0; num <= np - 1; num++)
                {
                    string input =
                        Microsoft.VisualBasic.Interaction.InputBox("Enter burst time: ",
                                                           "Burst time for P" + (num + 1),
                                                           "",
                                                           -1, -1);

                    bp[num] = Convert.ToInt64(input);
                }
                for (num = 0; num <= np - 1; num++)
                {
                    p[num] = bp[num];
                }
                for (x = 0; x <= np - 2; x++)
                {
                    for (num = 0; num <= np - 2; num++)
                    {
                        if (p[num] > p[num + 1])
                        {
                            temp = p[num];
                            p[num] = p[num + 1];
                            p[num + 1] = temp;
                        }
                    }
                }
                for (num = 0; num <= np - 1; num++)
                {
                    if (num == 0)
                    {
                        for (x = 0; x <= np - 1; x++)
                        {
                            if (p[num] == bp[x] && found == false)
                            {
                                wtp[num] = 0;
                                ct[num] = bp[x];
                                tat[num] = ct[num];
                                MessageBox.Show($"Process P{x + 1}:\nCompletion Time: {ct[num]} sec(s)\nTurnaround Time: {tat[num]} sec(s)\nWait Time: {wtp[num]} sec(s)",
                                                "Process Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                bp[x] = 0;
                                found = true;
                            }
                        }
                        found = false;
                    }
                    else
                    {
                        for (x = 0; x <= np - 1; x++)
                        {
                            if (p[num] == bp[x] && found == false)
                            {
                                wtp[num] = wtp[num - 1] + p[num - 1];
                                ct[num] = ct[num - 1] + bp[x];
                                tat[num] = ct[num];
                                MessageBox.Show($"Process P{x + 1}:\nCompletion Time: {ct[num]} sec(s)\nTurnaround Time: {tat[num]} sec(s)\nWait Time: {wtp[num]} sec(s)",
                                                "Process Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                bp[x] = 0;
                                found = true;
                            }
                        }
                        found = false;
                    }
                    twt += wtp[num];
                    ttat += tat[num];
                }

                awt = twt / np;
                atat = ttat / np;
                cpuUtilization = (ct[np - 1] / ct[np - 1]) * 100; // Always 100% since no idle time
                throughput = np / ct[np - 1];

                MessageBox.Show($"Summary:\nAverage Wait Time: {awt} sec(s)\nAverage Turnaround Time: {atat} sec(s)\nCPU Utilization: {cpuUtilization}%\nThroughput: {throughput} processes/sec",
                                "SJF Summary", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static void roundRobinAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);
            int i, counter = 0;
            double total = 0.0;
            double timeQuantum;
            double waitTime = 0, turnaroundTime = 0;
            double averageWaitTime, averageTurnaroundTime, cpuUtilization, throughput;
            double[] arrivalTime = new double[10];
            double[] burstTime = new double[10];
            double[] temp = new double[10];
            double[] completionTime = new double[10];
            int x = np;

            DialogResult result = MessageBox.Show("Round Robin Scheduling", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                for (i = 0; i < np; i++)
                {
                    string arrivalInput =
                            Microsoft.VisualBasic.Interaction.InputBox("Enter arrival time: ",
                                                               "Arrival time for P" + (i + 1),
                                                               "",
                                                               -1, -1);

                    arrivalTime[i] = Convert.ToInt64(arrivalInput);

                    string burstInput =
                            Microsoft.VisualBasic.Interaction.InputBox("Enter burst time: ",
                                                               "Burst time for P" + (i + 1),
                                                               "",
                                                               -1, -1);

                    burstTime[i] = Convert.ToInt64(burstInput);

                    temp[i] = burstTime[i];
                }
                string timeQuantumInput =
                            Microsoft.VisualBasic.Interaction.InputBox("Enter time quantum: ", "Time Quantum",
                                                               "",
                                                               -1, -1);

                timeQuantum = Convert.ToInt64(timeQuantumInput);
                Helper.QuantumTime = timeQuantumInput;

                for (total = 0, i = 0; x != 0;)
                {
                    if (temp[i] <= timeQuantum && temp[i] > 0)
                    {
                        total = total + temp[i];
                        temp[i] = 0;
                        counter = 1;
                    }
                    else if (temp[i] > 0)
                    {
                        temp[i] = temp[i] - timeQuantum;
                        total = total + timeQuantum;
                    }
                    if (temp[i] == 0 && counter == 1)
                    {
                        x--;
                        completionTime[i] = total;
                        double processTurnaroundTime = total - arrivalTime[i];
                        double processWaitTime = processTurnaroundTime - burstTime[i];

                        MessageBox.Show($"Process P{i + 1}:\nCompletion Time: {completionTime[i]} sec(s)\nTurnaround Time: {processTurnaroundTime} sec(s)\nWait Time: {processWaitTime} sec(s)",
                                        "Process Details", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        turnaroundTime += processTurnaroundTime;
                        waitTime += processWaitTime;
                        counter = 0;
                    }
                    if (i == np - 1)
                    {
                        i = 0;
                    }
                    else if (arrivalTime[i + 1] <= total)
                    {
                        i++;
                    }
                    else
                    {
                        i = 0;
                    }
                }

                averageWaitTime = waitTime / np;
                averageTurnaroundTime = turnaroundTime / np;
                cpuUtilization = (total / total) * 100; // Always 100% since no idle time
                throughput = np / total;

                MessageBox.Show($"Summary:\nAverage Wait Time: {averageWaitTime} sec(s)\nAverage Turnaround Time: {averageTurnaroundTime} sec(s)\nCPU Utilization: {cpuUtilization}%\nThroughput: {throughput} processes/sec",
                                "Round Robin Summary", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static void priorityAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);

            DialogResult result = MessageBox.Show("Priority Scheduling ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                double[] bp = new double[np];
                double[] wtp = new double[np];
                double[] tat = new double[np];
                double[] ct = new double[np];
                int[] p = new int[np];
                int[] sp = new int[np];
                int x, num;
                double twt = 0.0, ttat = 0.0, awt, atat, cpuUtilization, throughput;
                int temp = 0;
                bool found = false;

                for (num = 0; num < np; num++)
                {
                    string input = Microsoft.VisualBasic.Interaction.InputBox("Enter burst time: ",
                                                               "Burst time for P" + (num + 1),
                                                               "",
                                                               -1, -1);

                    bp[num] = Convert.ToInt64(input);
                }

                for (num = 0; num < np; num++)
                {
                    string input2 = Microsoft.VisualBasic.Interaction.InputBox("Enter priority: ",
                                                               "Priority for P" + (num + 1),
                                                               "",
                                                               -1, -1);

                    p[num] = Convert.ToInt16(input2);
                }

                for (num = 0; num < np; num++)
                {
                    sp[num] = p[num];
                }

                for (x = 0; x < np - 1; x++)
                {
                    for (num = 0; num < np - 1; num++)
                    {
                        if (sp[num] > sp[num + 1])
                        {
                            temp = sp[num];
                            sp[num] = sp[num + 1];
                            sp[num + 1] = temp;
                        }
                    }
                }

                for (num = 0; num < np; num++)
                {
                    if (num == 0)
                    {
                        for (x = 0; x < np; x++)
                        {
                            if (sp[num] == p[x] && found == false)
                            {
                                wtp[num] = 0;
                                ct[num] = bp[x];
                                tat[num] = ct[num];
                                MessageBox.Show($"Process P{x + 1}:\nCompletion Time: {ct[num]} sec(s)\nTurnaround Time: {tat[num]} sec(s)\nWait Time: {wtp[num]} sec(s)",
                                                "Process Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                temp = x;
                                p[x] = 0;
                                found = true;
                            }
                        }
                        found = false;
                    }
                    else
                    {
                        for (x = 0; x < np; x++)
                        {
                            if (sp[num] == p[x] && found == false)
                            {
                                wtp[num] = wtp[num - 1] + bp[temp];
                                ct[num] = ct[num - 1] + bp[x];
                                tat[num] = ct[num];
                                MessageBox.Show($"Process P{x + 1}:\nCompletion Time: {ct[num]} sec(s)\nTurnaround Time: {tat[num]} sec(s)\nWait Time: {wtp[num]} sec(s)",
                                                "Process Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                temp = x;
                                p[x] = 0;
                                found = true;
                            }
                        }
                        found = false;
                    }

                    twt += wtp[num];
                    ttat += tat[num];
                }

                awt = twt / np;
                atat = ttat / np;
                cpuUtilization = (ct[np - 1] / ct[np - 1]) * 100;
                throughput = np / ct[np - 1];

                MessageBox.Show($"Summary:\nAverage Wait Time: {awt} sec(s)\nAverage Turnaround Time: {atat} sec(s)\nCPU Utilization: {cpuUtilization}%\nThroughput: {throughput} processes/sec",
                                "Priority Scheduling Summary", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static void srtfAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);
            double[] arrivalTime = new double[np];
            double[] burstTime = new double[np];
            double[] remainingTime = new double[np];
            double[] completionTime = new double[np];
            double[] waitTime = new double[np];
            double[] turnaroundTime = new double[np];
            double totalWaitTime = 0.0, totalTurnaroundTime = 0.0, averageWaitTime, averageTurnaroundTime, cpuUtilization, throughput;
            int completed = 0, currentTime = 0, shortest = 0;
            bool found = false;

            DialogResult result = MessageBox.Show("Shortest Remaining Time First", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                for (int i = 0; i < np; i++)
                {
                    string arrivalInput = Microsoft.VisualBasic.Interaction.InputBox("Enter arrival time: ",
                                                           "Arrival time for P" + (i + 1),
                                                           "",
                                                           -1, -1);
                    arrivalTime[i] = Convert.ToDouble(arrivalInput);

                    string burstInput = Microsoft.VisualBasic.Interaction.InputBox("Enter burst time: ",
                                                           "Burst time for P" + (i + 1),
                                                           "",
                                                           -1, -1);
                    burstTime[i] = Convert.ToDouble(burstInput);
                    remainingTime[i] = burstTime[i];
                }

                while (completed != np)
                {
                    shortest = -1;
                    double minRemainingTime = double.MaxValue;

                    for (int i = 0; i < np; i++)
                    {
                        if (arrivalTime[i] <= currentTime && remainingTime[i] > 0 && remainingTime[i] < minRemainingTime)
                        {
                            minRemainingTime = remainingTime[i];
                            shortest = i;
                            found = true;
                        }
                    }

                    if (!found)
                    {
                        currentTime++;
                        continue;
                    }

                    remainingTime[shortest]--;
                    currentTime++;

                    if (remainingTime[shortest] == 0)
                    {
                        completed++;
                        completionTime[shortest] = currentTime;
                        turnaroundTime[shortest] = completionTime[shortest] - arrivalTime[shortest];
                        waitTime[shortest] = turnaroundTime[shortest] - burstTime[shortest];

                        totalWaitTime += waitTime[shortest];
                        totalTurnaroundTime += turnaroundTime[shortest];

                        MessageBox.Show($"Process P{shortest + 1}:\nCompletion Time: {completionTime[shortest]} sec(s)\nTurnaround Time: {turnaroundTime[shortest]} sec(s)\nWait Time: {waitTime[shortest]} sec(s)",
                                        "Process Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    found = false;
                }

                averageWaitTime = totalWaitTime / np;
                averageTurnaroundTime = totalTurnaroundTime / np;
                cpuUtilization = (currentTime / (double)currentTime) * 100;
                throughput = np / (double)currentTime;

                MessageBox.Show($"Summary:\nAverage Wait Time: {averageWaitTime} sec(s)\nAverage Turnaround Time: {averageTurnaroundTime} sec(s)\nCPU Utilization: {cpuUtilization}%\nThroughput: {throughput} processes/sec",
                                "SRTF Summary", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static void hrrnAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);
            double[] arrivalTime = new double[np];
            double[] burstTime = new double[np];
            double[] completionTime = new double[np];
            double[] waitTime = new double[np];
            double[] turnaroundTime = new double[np];
            bool[] isCompleted = new bool[np];
            double totalWaitTime = 0.0, totalTurnaroundTime = 0.0, averageWaitTime, averageTurnaroundTime, cpuUtilization, throughput;
            int completed = 0, currentTime = 0;

            DialogResult result = MessageBox.Show("Highest Response Ratio Next Scheduling", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                for (int i = 0; i < np; i++)
                {
                    string arrivalInput = Microsoft.VisualBasic.Interaction.InputBox("Enter arrival time: ",
                                                               "Arrival time for P" + (i + 1),
                                                               "",
                                                               -1, -1);
                    arrivalTime[i] = Convert.ToDouble(arrivalInput);

                    string burstInput = Microsoft.VisualBasic.Interaction.InputBox("Enter burst time: ",
                                                               "Burst time for P" + (i + 1),
                                                               "",
                                                               -1, -1);
                    burstTime[i] = Convert.ToDouble(burstInput);
                }

                while (completed != np)
                {
                    int selectedProcess = -1;
                    double maxResponseRatio = -1;

                    for (int i = 0; i < np; i++)
                    {
                        if (!isCompleted[i] && arrivalTime[i] <= currentTime)
                        {
                            double responseRatio = (currentTime - arrivalTime[i] + burstTime[i]) / burstTime[i];
                            if (responseRatio > maxResponseRatio)
                            {
                                maxResponseRatio = responseRatio;
                                selectedProcess = i;
                            }
                        }
                    }

                    if (selectedProcess == -1)
                    {
                        currentTime++;
                        continue;
                    }

                    currentTime += (int)burstTime[selectedProcess];
                    completionTime[selectedProcess] = currentTime;
                    turnaroundTime[selectedProcess] = completionTime[selectedProcess] - arrivalTime[selectedProcess];
                    waitTime[selectedProcess] = turnaroundTime[selectedProcess] - burstTime[selectedProcess];

                    totalWaitTime += waitTime[selectedProcess];
                    totalTurnaroundTime += turnaroundTime[selectedProcess];
                    isCompleted[selectedProcess] = true;
                    completed++;

                    MessageBox.Show($"Process P{selectedProcess + 1}:\nCompletion Time: {completionTime[selectedProcess]} sec(s)\nTurnaround Time: {turnaroundTime[selectedProcess]} sec(s)\nWait Time: {waitTime[selectedProcess]} sec(s)",
                                    "Process Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                averageWaitTime = totalWaitTime / np;
                averageTurnaroundTime = totalTurnaroundTime / np;
                cpuUtilization = (currentTime / (double)currentTime) * 100; // Always 100% since no idle time
                throughput = np / (double)currentTime;

                MessageBox.Show($"Summary:\nAverage Wait Time: {averageWaitTime} sec(s)\nAverage Turnaround Time: {averageTurnaroundTime} sec(s)\nCPU Utilization: {cpuUtilization}%\nThroughput: {throughput} processes/sec",
                                "HRRN Summary", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

