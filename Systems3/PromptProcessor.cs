using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PromptProcessor
{
    private PromptCache cache = new PromptCache();

    public void UpdateCacheParallel(List<(string prompt, float[] embedding)> newPrompts)
    {
        Parallel.ForEach(newPrompts, promptData =>
        {
            cache.AddPrompt(promptData.prompt, promptData.embedding);
        });
    }
}
