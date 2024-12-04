using System.ComponentModel;
using System.Text.Json;

List<int> report=new();
List<List<int>> reports=new();
int totalSafe=0;
var input=JsonSerializer.Deserialize<string>(File.ReadAllText("testInput.json"));


var lines=input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
foreach(var line in lines){
    report.Clear();
    string[] parts=line.Split(' ', '\t', StringSplitOptions.RemoveEmptyEntries);
    for (int i = 0; i < parts.Length; i++){
        report.Add(int.Parse(parts[i]));
    }
    bool check=IsSafe(report);
    if(check) totalSafe++;
    else{
        bool loop=false;
        while(loop==false){
            for (int i = 0; i < report.Count; i++){
                int intStorage=report[i];
                int index=i;
                report.RemoveAt(i);
                bool check2=IsSafe(report);
                if(check2){
                    totalSafe++;
                    loop=true;
                    i=report.Count;
                }
                else{
                    report.Insert(index,intStorage);
                }
            }
            loop=true;
        }
    }
}
Console.WriteLine(totalSafe);
Console.ReadLine();


bool IsSafe(List<int> report){
    if(report.Count==1){
        return false;
    }

    var firstDiff=report[0]-report[1];
    if(firstDiff==0||firstDiff>3){
        
        return false;
    }

    var expDer=firstDiff/Math.Abs(firstDiff);
    for (int i = 0; i < report.Count-1; i++){
        var diff=report[i]-report[i+1];
        if(diff==0||Math.Abs(diff)>3){
            return false;
        }
        
        var der=diff/Math.Abs(diff);
        if(der!=expDer){
            return false;
        }
    }
    
    return true;
}


/* Part 1
var lines=testInput.Split('\n', StringSplitOptions.RemoveEmptyEntries);
foreach(var line in lines){
    report.Clear();
    string[] parts=line.Split(' ', '\t', StringSplitOptions.RemoveEmptyEntries);
    for (int i = 0; i < parts.Length; i++){
        report.Add(int.Parse(parts[i]));
    }
    bool check=IsSafe(report);
    if(check) totalSafe++;
}
Console.WriteLine(totalSafe);
Console.ReadLine();
*/