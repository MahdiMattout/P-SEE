import React, { useState } from "react";
import './settings.css'
// import Form from './settingsForm'


function Settings(props) {

    return (props.trigger) ?
        (< div className="popup" >
            <div className="popup-inner">
                <button className="close-btn" onClick={() => props.setTrigger(false)}>
                    close
                </button>
                {props.children}
            </div>

        </div >) : "";
    
}


export default Settings
