import * as React from 'react';
import "../styles/SmallTeamLogo.css";
import "../styles/MainStyle.css";

export default function SmallTeamLogo({image}) {

    return (
        <div className="smallTeamLogo">
            <img className="smallTeamImage" src={image} alt="Small Team Logo" />
                
        </div>
    )
}