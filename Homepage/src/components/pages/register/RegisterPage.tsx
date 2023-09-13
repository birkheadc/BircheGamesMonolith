import * as React from 'react';
import './RegisterPage.css'
import UserRegisterForm from './userRegisterForm/UserRegisterForm';
import INewUser from '../../../types/user/newUser/newUser';
import api from '../../../api';
import WorkingOverlay from '../../shared/workingOverlay/WorkingOverlay';

interface IRegisterPageProps {

}

/**
*
* @returns {JSX.Element | null}
*/
export default function RegisterPage(props: IRegisterPageProps): JSX.Element | null {

  const [working, setWorking] = React.useState<boolean>(false);
  const [message, setMessage] = React.useState<string | null>(null);

  const handleSubmitNewUser = async (user: INewUser) => {
    setWorking(true);
    const response = await api.registration.createUser(user);
    setWorking(false);
    return response;
  } 

  return (
    <div className='register-page-wrapper'>
      <div className='register-page-form-wrapper'>
        <WorkingOverlay element={<UserRegisterForm submit={handleSubmitNewUser}/>} isWorking={working} />
      </div>
    </div>
  );
}