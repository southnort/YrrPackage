namespace Yrr.UI.Core
{
    internal interface IScreenFactory<in TKey, out TValue> where TValue : IScreen
    {
        TValue GetScreen(TKey key);
    }
}