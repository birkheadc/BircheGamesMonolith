import * as React from 'react';
import './LogoutPage.css'
import { useNavigate } from 'react-router-dom';

interface ILogoutPageProps {
  logout: () => void
}

/**
*
* @returns {JSX.Element | null}
*/
export default function LogoutPage(props: ILogoutPageProps): JSX.Element | null {

  React.useEffect(function logoutOnMount() {
    props.logout();
  }, [props.logout]);

  return (
    <div className='logout-page-wrapper'>
      
    </div>
  );
}