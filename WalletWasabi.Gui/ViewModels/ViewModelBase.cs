using ReactiveUI;
using System;
using System.Collections;
using System.ComponentModel;
using WalletWasabi.Gui.Validation;

namespace WalletWasabi.Gui.ViewModels
{
	public class ViewModelBase : ReactiveObject, INotifyDataErrorInfo, IRegisterValidationMethod
	{
		private Validations _validations;

		public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

		public ViewModelBase()
		{
			_validations = new Validations();
			_validations.ErrorsChanged += OnValidations_ErrorsChanged;
			PropertyChanged += ViewModelBase_PropertyChanged;
		}

		protected IValidations Validations => _validations;

		private void OnValidations_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
		{
			ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(e.PropertyName));
		}

		private void ViewModelBase_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(e.PropertyName))
			{
				_validations.Validate();
			}
			else
			{
				_validations.ValidateProperty(e.PropertyName);
			}
		}

		bool INotifyDataErrorInfo.HasErrors => (_validations as IValidations).Any;

		IEnumerable INotifyDataErrorInfo.GetErrors(string propertyName)
		{
			return _validations.GetErrors(propertyName);
		}

		void IRegisterValidationMethod.RegisterValidationMethod(string propertyName, ValidateMethod validateMethod)
		{
			((IRegisterValidationMethod)_validations).RegisterValidationMethod(propertyName, validateMethod);
		}
	}
}
