# Modified CPU-Simulator GUI for CS3502 
## Overview
This project is a modified Windows Forms-based application built in C# that implements six different CPU scheduling algorithms:

- **First-Come-First-Serve (FCFS)**
- **Shortest Job First (SJF)**
- **Round Robin (RR)**
- **Priority Scheduling**
- **Shortest Remaining Time First (SRTF)**
- **Highest Response Ratio Next (HRRN)**

The simulator allows users to input process information (arrival time, burst time, and priority where needed) through prompt boxes, and calculates key performance metrics, including:

- Average Waiting Time (AWT)
- Average Turnaround Time (ATT)
- CPU Utilization
- Throughput

Each process’s completion time, turnaround time, and waiting time are displayed individually during simulation, followed by a summary.

---

## How to Run

1. **Clone the repository** or download the project files.
2. **Open the solution** (`.sln` file) using **Visual Studio**.
3. **Build the solution** (`Ctrl+Shift+B`) to ensure all dependencies are installed.
4. **Run the project** (`F5` or click the green "Start" button).
5. **Select the scheduling algorithm** you want to simulate.
6. **Enter the required process details** when prompted:
   - Arrival Time
   - Burst Time
   - (Priority, if using Priority Scheduling)
   - (Time Quantum, if using Round Robin Scheduling)
7. **View the results**: Individual process results and a final summary window will appear at the end of each run.

---

## Requirements
- **.NET Framework** (or **.NET Core** with Windows Forms support)
- **Visual Studio 2019/2022** (recommended)
- **Windows OS** (required for Windows Forms)

---

## Notes
- CPU utilization is assumed to be 100% in this simulator unless there are explicit idle gaps between process executions.
- Time is measured in abstract **seconds** (units) for easier interpretation.

---
