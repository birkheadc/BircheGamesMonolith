import * as React from 'react';
import './UserRegisterForm.css'
import INewUser from '../../../../types/user/newUser/newUser';
import ICreateUserResponse from '../../../../types/user/newUser/createUserResponse';
import api from '../../../../api';

interface IUserRegisterFormProps {
  submit: (user: INewUser) => Promise<ICreateUserResponse>,
  errorMessages: string[]
}

interface IUserRegisterFormErrors {
  emailAddress: boolean,
  password: boolean,
  repeatPassword: boolean
}

/**
*
* @returns {JSX.Element | null}
*/
export default function UserRegisterForm(props: IUserRegisterFormProps): JSX.Element | null {

  const [user, setUser] = React.useState<INewUser>({
    emailAddress: '',
    password: '',
    repeatPassword: ''
  });

  const [errors, setErrors] = React.useState<IUserRegisterFormErrors>({
    emailAddress: false,
    password: false,
    repeatPassword: false
  });

  const [isValid, setValid] = React.useState<boolean>(false);

  React.useEffect(function validateLocallyOnUserChange() {
    const response = api.registration.validateLocal(user);
    setValid(response.wasSuccess);
    const newErrors: IUserRegisterFormErrors = {
      emailAddress: false,
      password: false,
      repeatPassword: false
    };
    response.errors.forEach(error => {
      const field = error.field[0]?.toLowerCase() + error.field.substring(1);
      newErrors[field] = true;
    });
    setErrors(newErrors);
  }, [ user ]);

  const handleSubmit = async (event: React.FormEvent) => {
    event.preventDefault();
    (document.activeElement as HTMLElement)?.blur();
    const response = await props.submit(user);
    if (response.wasSuccess === false) {
      setErrors(errors => {
        const newErrors = {...errors};
        response.errors.forEach(error => {
          const field = error.field[0]?.toLowerCase() + error.field.substring(1);
          newErrors[field] = true;
        });
        return newErrors;
      });
    }
  }

  const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const name: string = event.currentTarget.name;
    const value: string = event.currentTarget.value;
    const newUser = {...user};
    newUser[name] = value;
    setUser(newUser);
  }

  return (
    <form className='standard-form' onSubmit={handleSubmit}>
      <h1>Create Account</h1>
      {(props.errorMessages.length > 0) && 
        <ul className='standard-form-errors'>
          {props.errorMessages.map(
            message =>
            <li key={message}>{message}</li>
          )}
        </ul>
      }
      <div className='form-row'>
        <div className='label-input-wrapper'>
          <label className={errors.emailAddress ? 'error' : ''} htmlFor='email'>Email Address</label>
          <input id='email' type='email' name='emailAddress' onChange={handleChange} value={user.emailAddress}></input>
        </div>
      </div>
      <div className='form-row gap-large'>
        <div className='label-input-wrapper'>
          <label className={errors.password ? 'error' : ''} htmlFor='password'>Password</label>
          <input id='password' type='password' name='password' onChange={handleChange}></input>
        </div>
        <div className='label-input-wrapper'>
          <label className={errors.repeatPassword ? 'error' : ''} htmlFor='password-repeat'>Repeat Password</label>
          <input id='password-repeat' type='password' name='repeatPassword' onChange={handleChange}></input>
        </div>
      </div>
      <div className="form-row form-buttons-row">
        <button disabled={!isValid} type='submit'>Submit</button>
      </div>
    </form>
  );
}