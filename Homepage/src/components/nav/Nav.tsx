import * as React from 'react';
import './Nav.css'
import { NavLink } from 'react-router-dom';
import { IUserDTO } from '../../types/user/user';
import { UserContext } from '../../contexts/userContext/UserContext';

interface INavProps {
  
}

/**
*
* @returns {JSX.Element | null}
*/
export default function Nav(props: INavProps): JSX.Element | null {

  const { loggedInUser } = React.useContext(UserContext);

  return (
    <nav>
      <ul>
        <NavLink className={navLinkClass} to={'/'}>Home</NavLink>
        
      </ul>
      <ul>
        { loggedInUser == null && <NavLink className={navLinkClass} to={'/register'}>Create Account</NavLink>}
        { loggedInUser != null && <NavLink className={navLinkClass} to={'/account'}>{loggedInUser.isDisplayNameChosen ? `${loggedInUser.displayName}#${loggedInUser.tag}` : 'Account'}</NavLink>}
        { loggedInUser == null && <NavLink className={navLinkClass} to={'/login'}>Login</NavLink>}
        { loggedInUser != null && <NavLink className={navLinkClass} to={'/logout'} >Logout</NavLink>}
      </ul>
    </nav>
  );
}

// Helpers

const navLinkClass = ({ isActive }: { isActive: boolean }) => {
  return "navlink" + ( isActive ? " active" : " inactive" )
}