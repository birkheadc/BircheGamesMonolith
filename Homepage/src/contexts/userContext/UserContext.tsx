import * as React from 'react';
import { IUserDTO } from '../../types/user/user';

type Props = {
  children: React.ReactNode
}

type State = {
  loggedInUser: IUserDTO | null | undefined,
  setLoggedInUser: React.Dispatch<React.SetStateAction<IUserDTO | null | undefined>>
};

const defaultUser: IUserDTO = {
  id: '',
  emailAddress: '',
  displayName: '',
  tag: '',
  creationDateTime: '',
  role: 0,
  isEmailVerified: false,
  isDisplayNameChosen: false
};
export const UserContext = React.createContext<State>({loggedInUser: defaultUser, setLoggedInUser: () => {}});

export const UserProvider = ({ children }: Props) => {
  const [ loggedInUser, setLoggedInUser ] = React.useState<IUserDTO | null | undefined>(undefined);
  return (
    <UserContext.Provider value={{ loggedInUser, setLoggedInUser }}>
      { children }
    </UserContext.Provider>
  )
}