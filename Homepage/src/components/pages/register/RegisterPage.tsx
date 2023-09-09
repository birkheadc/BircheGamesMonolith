import * as React from 'react';
import './RegisterPage.css'
import UserRegisterForm from './userRegisterForm/UserRegisterForm';
import INewUser from '../../../types/user/newUser/newUser';
import api from '../../../api';

interface IRegisterPageProps {

}

/**
*
* @returns {JSX.Element | null}
*/
export default function RegisterPage(props: IRegisterPageProps): JSX.Element | null {

  const handleSubmitNewUser = async (user: INewUser) => {
    const response = await api.registration.createUser(user);
    console.log(response);
  } 

  return (
    <main>
      <UserRegisterForm submit={handleSubmitNewUser}/>
    </main>
  );
}