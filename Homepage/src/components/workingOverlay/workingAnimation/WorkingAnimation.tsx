import * as React from 'react';
import './WorkingAnimation.css';
import icon from '../../../assets/favicon.png';

interface IWorkingAnimationProps {

}

/**
*
* @returns {JSX.Element | null}
*/
export default function WorkingAnimation(props: IWorkingAnimationProps): JSX.Element | null {
  return (
    <div className='working-animation-wrapper'>
      <img src={icon}></img>
    </div>
  );
}