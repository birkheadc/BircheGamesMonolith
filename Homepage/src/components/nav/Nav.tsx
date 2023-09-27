import * as React from 'react';
import './Nav.css'
import { NavLink } from 'react-router-dom';
import { IUserDTO } from '../../types/user/user';

interface INavProps {
  user: IUserDTO | null
}

/**
*
* @returns {JSX.Element | null}
*/
export default function Nav(props: INavProps): JSX.Element | null {
  return (
    <nav>
      <ul>
        <NavLink className={navLinkClass} to={'/'}>Home</NavLink>
        { props.user == null && <NavLink className={navLinkClass} to={'/register'}>Create Account</NavLink>}
        { props.user != null && <NavLink className={navLinkClass} to={'/account'}>Account</NavLink>}
        { props.user == null && <NavLink className={navLinkClass} to={'/login'}>Login</NavLink>}
        { props.user != null && <NavLink className={navLinkClass} to={'/logout'} >Logout</NavLink>}
      </ul>
    </nav>
  );
}

// Helpers

const navLinkClass = ({ isActive }) => {
  return "navlink" + ( isActive ? " active" : " inactive" )
}