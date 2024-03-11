using NJsonSchema.CodeGeneration;
using NSwag.CodeGeneration.TypeScript;
using NSwag;

namespace Web.Code
{
	// https://github.com/RicoSuter/NSwag/issues/3580
	public class BugfixedTypeScriptClientGenerator : TypeScriptClientGenerator
	{
		public BugfixedTypeScriptClientGenerator(OpenApiDocument document, TypeScriptClientGeneratorSettings settings) : base(document, settings)
		{
		}

		protected override IEnumerable<CodeArtifact> GenerateDtoTypes()
		{
			var types = base.GenerateDtoTypes();
			foreach (var t in types)
			{
				if (t.BaseTypeName == "DatabaseTable")
				{
					var ts = t.Code;
					var fixedTs = ts.Replace("super(data);\n", "super(data);\n        if (data) {\n            for (var property in data) {\n                if (data.hasOwnProperty(property))\n                    (<any>this)[property] = (<any>data)[property];\n            }\n        }\n");
					yield return new CodeArtifact(t.TypeName, t.BaseTypeName, CodeArtifactType.Class, CodeArtifactLanguage.TypeScript, CodeArtifactCategory.Contract, fixedTs);
				}
				else
				{
					yield return t;
				}
			}
		}
	}
}
