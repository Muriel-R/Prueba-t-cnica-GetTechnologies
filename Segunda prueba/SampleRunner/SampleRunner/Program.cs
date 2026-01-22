using System;
using System.IO;
using Sample;

Console.WriteLine("=== SampleRunner: Demo UpdateFiles ===");

var root = @"C:\MarcoRamirez\SampleUpdaterDemo";
var source = Path.Combine(root, "source");
var target = Path.Combine(root, "target");
var backup = Path.Combine(root, "backup");

Directory.CreateDirectory(source);
Directory.CreateDirectory(target);
Directory.CreateDirectory(backup);


// 1) Creamos un archivo .add 
var relDir = "docs";
Directory.CreateDirectory(Path.Combine(source, relDir));

var addFile = Path.Combine(source, relDir, "hello.txt.add");
File.WriteAllText(addFile, "hola mundo desde .add");

var fm = new FileManagerSample();
var result = fm.UpdateFiles(source, target, backup); // usa FileUpdater.UpdateFiles :contentReference[oaicite:9]{index=9}

Console.WriteLine(string.IsNullOrEmpty(result) ? "UpdateFiles OK" : $"UpdateFiles ERROR:\n{result}");

// 2) Validación / evidencia
var expected = Path.Combine(target, relDir, "hello.txt");
Console.WriteLine($"Archivo esperado: {expected}");
Console.WriteLine(File.Exists(expected) ? " Existe en target" : " NO existe en target");

// 3) probamos delete:
var delFile = Path.Combine(source, relDir, "hello.txt.del");
File.WriteAllText(delFile, string.Empty);

result = fm.UpdateFiles(source, target, backup);
Console.WriteLine(string.IsNullOrEmpty(result) ? "Delete OK" : $"Delete ERROR:\n{result}");
Console.WriteLine(File.Exists(expected) ? " Sigue existiendo" : " Ya se eliminó");

// 4) Imprime dónde quedó todo 
Console.WriteLine("\nFolders:");
Console.WriteLine($"root:   {root}");
Console.WriteLine($"source: {source}");
Console.WriteLine($"target: {target}");
Console.WriteLine($"backup: {backup}");

Console.WriteLine("\n=== Fin demo ===");
