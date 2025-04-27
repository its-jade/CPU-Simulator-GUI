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

            double[] arrivalTime = new double[np];
            double[] bp = new double[np];
            double[] wtp = new double[np];
            double[] tat = new double[np];
            double[] ct = new double[np];
            double[] remainingBp = new double[np];
            double twt = 0.0, ttat = 0.0, awt, atat, cpuUtilization, throughput;
            int num;

            DialogResult result = MessageBox.Show("Shortest Job First Scheduling", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

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
                    remainingBp[num] = bp[num];
                }

                for (int i = 0; i < np - 1; i++)
                {
                    for (int j = 0; j < np - i - 1; j++)
                    {
                        if (arrivalTime[j] > arrivalTime[j + 1])
                        {
                            double temp = arrivalTime[j];
                            arrivalTime[j] = arrivalTime[j + 1];
                            arrivalTime[j + 1] = temp;

                            temp = bp[j];
                            bp[j] = bp[j + 1];
                            bp[j + 1] = temp;
                        }
                    }
                }

                double currentTime = 0;
                bool[] completed = new bool[np];
                int completedCount = 0;

                while (completedCount < np)
                {
                    int idx = -1;
                    double minBurst = double.MaxValue;

                    for (int i = 0; i < np; i++)
                    {
                        if (!completed[i] && arrivalTime[i] <= currentTime)
                        {
                            if (bp[i] < minBurst)
                            {
                                minBurst = bp[i];
                                idx = i;
                            }
                        }
                    }

                    if (idx != -1)
                    {
                        currentTime += bp[idx];
                        ct[idx] = currentTime;
                        tat[idx] = ct[idx] - arrivalTime[idx];
                        wtp[idx] = tat[idx] - remainingBp[idx];

                        completed[idx] = true;
                        completedCount++;

                        MessageBox.Show($"Process P{idx + 1}:\nArrival Time: {arrivalTime[idx]} sec(s)\nCompletion Time: {ct[idx]} sec(s)\nTurnaround Time: {tat[idx]} sec(s)\nWait Time: {wtp[idx]} sec(s)",
                                        "Process Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        currentTime++;
                    }
                }

                for (num = 0; num < np; num++)
                {
                    twt += wtp[num];
                    ttat += tat[num];
                }

                awt = twt / np;
                atat = ttat / np;
                cpuUtilization = ((currentTime - arrivalTime.Min()) / currentTime) * 100;
                throughput = np / (currentTime - arrivalTime.Min());

                MessageBox.Show($"Summary:\nAverage Wait Time: {awt} sec(s)\nAverage Turnaround Time: {atat} sec(s)\nCPU Utilization: {cpuUtilization}%\nThroughput: {throughput} processes/sec",
                                "SJF Summary", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static void roundRobinAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);
            double[] arrivalTime = new double[np];
            double[] bp = new double[np];
            double[] remainingTime = new double[np];
            double[] wtp = new double[np];
            double[] tat = new double[np];
            double[] ct = new double[np];
            double twt = 0.0, ttat = 0.0, awt, atat, cpuUtilization, throughput;
            int num;

            DialogResult result = MessageBox.Show("Round Robin Scheduling", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

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

                    remainingTime[num] = bp[num]; 
                }

                string tqInput = Microsoft.VisualBasic.Interaction.InputBox("Enter Time Quantum: ",
                                                            "Time Quantum",
                                                            "",
                                                            -1, -1);
                double timeQuantum = Convert.ToDouble(tqInput);

                double currentTime = 0;
                int completed = 0;
                bool[] isCompleted = new bool[np];

                while (completed != np)
                {
                    bool foundProcess = false;

                    for (int i = 0; i < np; i++)
                    {
                        if (arrivalTime[i] <= currentTime && remainingTime[i] > 0)
                        {
                            foundProcess = true;

                            if (remainingTime[i] <= timeQuantum)
                            {
                                currentTime += remainingTime[i];
                                remainingTime[i] = 0;
                                ct[i] = currentTime;
                                completed++;
                            }
                            else
                            {
                                currentTime += timeQuantum;
                                remainingTime[i] -= timeQuantum;
                            }
                        }
                    }

                    if (!foundProcess)
                    {
                        currentTime++;
                    }
                }

                for (int i = 0; i < np; i++)
                {
                    tat[i] = ct[i] - arrivalTime[i];
                    wtp[i] = tat[i] - bp[i];
                    if (wtp[i] < 0) wtp[i] = 0;
                    twt += wtp[i];
                    ttat += tat[i];

                    MessageBox.Show($"Process P{i + 1}:\nArrival Time: {arrivalTime[i]} sec(s)\nCompletion Time: {ct[i]} sec(s)\nTurnaround Time: {tat[i]} sec(s)\nWait Time: {wtp[i]} sec(s)",
                                    "Process Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                awt = twt / np;
                atat = ttat / np;
                cpuUtilization = ((ct.Max() - arrivalTime.Min()) / ct.Max()) * 100;
                throughput = np / (ct.Max() - arrivalTime.Min());

                MessageBox.Show($"Summary:\nAverage Wait Time: {awt} sec(s)\nAverage Turnaround Time: {atat} sec(s)\nCPU Utilization: {cpuUtilization}%\nThroughput: {throughput} processes/sec",
                                "Round Robin Summary", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static void priorityAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);
            double[] arrivalTime = new double[np];
            double[] burstTime = new double[np];
            int[] priority = new int[np];
            bool[] isCompleted = new bool[np];
            double[] completionTime = new double[np];

            double waitTime = 0, turnaroundTime = 0;

            DialogResult result = MessageBox.Show("Priority Scheduling", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                for (int i = 0; i < np; i++)
                {
                    arrivalTime[i] = Convert.ToDouble(Microsoft.VisualBasic.Interaction.InputBox($"Enter arrival time for P{i + 1}:"));
                    burstTime[i] = Convert.ToDouble(Microsoft.VisualBasic.Interaction.InputBox($"Enter burst time for P{i + 1}:"));
                    priority[i] = Convert.ToInt32(Microsoft.VisualBasic.Interaction.InputBox($"Enter priority for P{i + 1}:"));
                }

                double currentTime = 0;
                int completed = 0;

                while (completed < np)
                {
                    int idx = -1;
                    int minPriority = int.MaxValue;

                    for (int i = 0; i < np; i++)
                    {
                        if (arrivalTime[i] <= currentTime && !isCompleted[i])
                        {
                            if (priority[i] < minPriority)
                            {
                                minPriority = priority[i];
                                idx = i;
                            }
                        }
                    }

                    if (idx != -1)
                    {
                        currentTime += burstTime[idx];
                        completionTime[idx] = currentTime;
                        double tat = completionTime[idx] - arrivalTime[idx];
                        double wt = tat - burstTime[idx];
                        if (wt < 0) wt = 0; // Prevent negative wait times

                        turnaroundTime += tat;
                        waitTime += wt;

                        isCompleted[idx] = true;
                        completed++;

                        MessageBox.Show($"Process P{idx + 1}:\nCompletion Time: {completionTime[idx]} sec(s)\nTurnaround Time: {tat} sec(s)\nWait Time: {wt} sec(s)", "Process Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        currentTime++;
                    }
                }

                double awt = waitTime / np;
                double att = turnaroundTime / np;
                double cpuUtilization = ((completionTime.Max() - arrivalTime.Min()) / completionTime.Max()) * 100;
                double throughput = np / (completionTime.Max() - arrivalTime.Min());

                MessageBox.Show($"Summary:\nAverage Wait Time: {awt} sec(s)\nAverage Turnaround Time: {att} sec(s)\nCPU Utilization: {cpuUtilization}%\nThroughput: {throughput} processes/sec", "Priority Summary", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            double totalWaitTime = 0.0, totalTurnaroundTime = 0.0;
            int completed = 0, currentTime = 0;

            DialogResult result = MessageBox.Show("Shortest Remaining Time First", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                for (int i = 0; i < np; i++)
                {
                    arrivalTime[i] = Convert.ToDouble(Microsoft.VisualBasic.Interaction.InputBox($"Enter arrival time for P{i + 1}:"));
                    burstTime[i] = Convert.ToDouble(Microsoft.VisualBasic.Interaction.InputBox($"Enter burst time for P{i + 1}:"));
                    remainingTime[i] = burstTime[i];
                }

                while (completed != np)
                {
                    int shortest = -1;
                    double minRemainingTime = double.MaxValue;
                    bool found = false;

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
                        if (waitTime[shortest] < 0) waitTime[shortest] = 0;

                        totalWaitTime += waitTime[shortest];
                        totalTurnaroundTime += turnaroundTime[shortest];

                        MessageBox.Show($"Process P{shortest + 1}:\nCompletion Time: {completionTime[shortest]} sec(s)\nTurnaround Time: {turnaroundTime[shortest]} sec(s)\nWait Time: {waitTime[shortest]} sec(s)", "Process Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                double averageWaitTime = totalWaitTime / np;
                double averageTurnaroundTime = totalTurnaroundTime / np;
                double cpuUtilization = ((completionTime.Max() - arrivalTime.Min()) / completionTime.Max()) * 100;
                double throughput = np / (completionTime.Max() - arrivalTime.Min());

                MessageBox.Show($"Summary:\nAverage Wait Time: {averageWaitTime} sec(s)\nAverage Turnaround Time: {averageTurnaroundTime} sec(s)\nCPU Utilization: {cpuUtilization}%\nThroughput: {throughput} processes/sec", "SRTF Summary", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static void hrrnAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);
            double[] arrivalTime = new double[np];
            double[] burstTime = new double[np];
            bool[] isCompleted = new bool[np];
            double[] completionTime = new double[np];

            double totalWaitTime = 0, totalTurnaroundTime = 0;
            int completed = 0;
            double currentTime = 0;

            DialogResult result = MessageBox.Show("Highest Response Ratio Next Scheduling", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                for (int i = 0; i < np; i++)
                {
                    arrivalTime[i] = Convert.ToDouble(Microsoft.VisualBasic.Interaction.InputBox($"Enter arrival time for P{i + 1}:"));
                    burstTime[i] = Convert.ToDouble(Microsoft.VisualBasic.Interaction.InputBox($"Enter burst time for P{i + 1}:"));
                }

                while (completed < np)
                {
                    int idx = -1;
                    double highestRR = -1;

                    for (int i = 0; i < np; i++)
                    {
                        if (!isCompleted[i] && arrivalTime[i] <= currentTime)
                        {
                            double rr = (currentTime - arrivalTime[i] + burstTime[i]) / burstTime[i];
                            if (rr > highestRR)
                            {
                                highestRR = rr;
                                idx = i;
                            }
                        }
                    }

                    if (idx != -1)
                    {
                        currentTime += burstTime[idx];
                        completionTime[idx] = currentTime;
                        double tat = completionTime[idx] - arrivalTime[idx];
                        double wt = tat - burstTime[idx];
                        if (wt < 0) wt = 0;

                        totalTurnaroundTime += tat;
                        totalWaitTime += wt;

                        isCompleted[idx] = true;
                        completed++;

                        MessageBox.Show($"Process P{idx + 1}:\nCompletion Time: {completionTime[idx]} sec(s)\nTurnaround Time: {tat} sec(s)\nWait Time: {wt} sec(s)", "Process Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        currentTime++;
                    }
                }

                double awt = totalWaitTime / np;
                double att = totalTurnaroundTime / np;
                double cpuUtilization = ((completionTime.Max() - arrivalTime.Min()) / completionTime.Max()) * 100;
                double throughput = np / (completionTime.Max() - arrivalTime.Min());

                MessageBox.Show($"Summary:\nAverage Wait Time: {awt} sec(s)\nAverage Turnaround Time: {att} sec(s)\nCPU Utilization: {cpuUtilization}%\nThroughput: {throughput} processes/sec", "HRRN Summary", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}

