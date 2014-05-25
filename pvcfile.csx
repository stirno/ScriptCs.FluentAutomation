pvc.Task("nuget-push", () => {
	pvc.Source("ScriptCs.FluentAutomation.csproj")
	   .Pipe(new PvcNuGetPack(
			createSymbolsPackage: true
	   ))
	   .Pipe(new PvcNuGetPush());
});