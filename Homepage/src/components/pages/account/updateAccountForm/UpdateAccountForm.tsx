import * as React from 'react';
import './UpdateAccountForm.css'
import { IUserDTO } from '../../../../types/user/user';
import { IUpdateUserRequest } from '../../../../types/user/updateUser/updateUserRequest';

interface IUpdateAccountFormProps {
  user: IUserDTO
}

/**
*
* @returns {JSX.Element | null}
*/
export default function UpdateAccountForm(props: IUpdateAccountFormProps): JSX.Element | null {

  const user = props.user;

  const [ request, setRequest ] = React.useState<IUpdateUserRequest>({
    displayName: user.displayName,
    tag: user.tag
  });

  React.useEffect(function clearDisplayNameAndTagIfNotYetChosen() {
    if (user.isDisplayNameChosen === false) {
      setRequest(r => ({ ...r, displayName: '', tag: '' }));
    }
  }, [ user ]);

  const handleChange = (event: React.FormEvent<HTMLInputElement>) => {
    const name = event.currentTarget.name;
    const value = event.currentTarget.value;
    setRequest(r => {
      const newRequest = {...r};
      newRequest[name] = value;
      return newRequest;
    })
  }

  return (
    <form className='update-account-form standard-form'>
      <div className='form-row'>
        <div className='label-input-wrapper disabled'>
          <label htmlFor='email'>Email Address</label>
          <input disabled={true} id='email' type='email' name='emailAddress' value={user.emailAddress}></input>
        </div>
      </div>
      <div className='form-row'>
      <div className='label-input-wrapper'>
          <label htmlFor='display-name'>Display Name</label>
          <input id='display-name' name='displayName' onChange={handleChange} value={request.displayName}></input>
        </div>
        <span className='display-name-hash-sign'>#</span>
        <div className='label-input-wrapper'>
          <label htmlFor='tag'>Tag</label>
          <input id='tag' name='tag' onChange={handleChange} value={request.tag}></input>
        </div>
      </div>
    </form>
  );
}