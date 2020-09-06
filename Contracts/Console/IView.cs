namespace Contracts.Console
{
    public interface IView<in T> : IView
    {
        void RenderView(T viewModel);
    }

    public interface IView
    {
        void RenderView();
    }
}