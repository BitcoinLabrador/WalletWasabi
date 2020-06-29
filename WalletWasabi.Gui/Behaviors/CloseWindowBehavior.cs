using Avalonia;
using Avalonia.Controls;
using Avalonia.Xaml.Interactivity;
using ReactiveUI;
using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace WalletWasabi.Gui.Behaviors
{
	public class CloseWindowCommandBehavior : CommandBasedBehavior<Window>
	{
		private CompositeDisposable Disposables { get; } = new CompositeDisposable();

		protected override void OnAttached()
		{
			base.OnAttached();

			this.GetObservable(CommandProperty)
				.Where(cmd => cmd is { })
				.Subscribe(cmd => ObservableExtensions.Subscribe(
						(ReactiveCommand<Unit, Unit>)cmd,
						_ => AssociatedObject.Close()))
				.DisposeWith(Disposables);
		}

		protected override void OnDetaching()
		{
			Disposables?.Dispose();
			base.OnDetaching();
		}
	}
}
