import * as React from 'react';
import './RegisterPage.css'
import UserRegisterForm from './userRegisterForm/UserRegisterForm';
import INewUser from '../../../types/user/newUser/newUser';
import api from '../../../api';
import WorkingOverlay from '../../shared/workingOverlay/WorkingOverlay';
import { createSearchParams, useNavigate } from 'react-router-dom';

interface IRegisterPageProps {
  sendVerificationEmail: (address: string) => void
}

/**
*
* @returns {JSX.Element | null}
*/
export default function RegisterPage(props: IRegisterPageProps): JSX.Element | null {

  const [working, setWorking] = React.useState<boolean>(false);
  const [errors, setErrors] = React.useState<string[]>([]);

  const nav = useNavigate();

  const handleSubmitNewUser = async (user: INewUser) => {
    setWorking(true);
    const response = await api.registration.createUser(user);
    setWorking(false);
    if (response.wasSuccess) {
      props.sendVerificationEmail(user.emailAddress);
      nav({ pathname: '/email-verification/generate', search: `?${createSearchParams({ address: user.emailAddress })}` });
    } else {
      const _errors: string[] = [];
      response.errors.map(
        error => {
          if (error.errorMessage) _errors.push(error.errorMessage);
        }
      )
      setErrors(_errors);
    }
    return response;
  } 

  return (
    <div className='register-page-wrapper'>
      <div className='register-page-form-wrapper'>
        <WorkingOverlay element={<UserRegisterForm submit={handleSubmitNewUser} errorMessages={errors}/>} isWorking={working} />
      </div>
    </div>
  );
}