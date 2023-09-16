import * as React from 'react';
import './Nav.css'
import { NavLink } from 'react-router-dom';

interface INavProps {

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
        <NavLink className={navLinkClass} to={'/register'}>Create Account</NavLink>
        <NavLink className={navLinkClass} to={'/login'}>Login</NavLink>
      </ul>
    </nav>
  );
}

// Helpers

const navLinkClass = ({ isActive }) => {
  return "navlink" + ( isActive ? " active" : " inactive" )
}