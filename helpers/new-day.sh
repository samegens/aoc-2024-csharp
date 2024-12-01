#!/bin/bash

set -euo pipefail

show_help() {
    echo "Usage: $0 DAY"
    echo "This script creates two directories, dayDAY-clean and dayDAY-fast, and sets up .NET solutions and projects in them."
    echo "DAY should be the day number for which you want to set up the directories and projects."
}

if [ "$#" -ne 1 ]; then
    show_help
    exit 1
fi

DAY=$1

echo "Creating and setting up day$DAY..."
mkdir "day$DAY"
cd "day$DAY"
dotnet new sln -n "day$DAY"
dotnet new console -n Solver
dotnet sln "day$DAY.sln" add Solver/Solver.csproj
dotnet new nunit -n SolverTests
dotnet sln "day$DAY.sln" add SolverTests/SolverTests.csproj
dotnet add SolverTests/SolverTests.csproj reference Solver/Solver.csproj
touch Solver/input.txt
cp ../helpers/*.cs Solver/
rm SolverTests/*.cs
cp ../helpers/SolverTests.cs/*.cs SolverTests
