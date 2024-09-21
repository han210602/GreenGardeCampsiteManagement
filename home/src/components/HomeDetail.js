import React from 'react';

import './HomeDetailStyle.css';
const HomeDetail = (props) => {
    return (
        <>
            <div className={props.cName}> 
                <img alt="img" src={props.img}/>  
            </div>


            <div className='img-text'>
                <h1>{props.title}</h1>  
                <p>{props.text}</p>    
                <a href={props.url}className={props.btnClass} >{props.buttonText}</a>  
                
            </div>
        </>
    );
}

export default HomeDetail;
