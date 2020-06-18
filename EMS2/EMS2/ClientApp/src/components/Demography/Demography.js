import React from 'react';
import { Link, NavLink} from 'react-router-dom'
const Demography = () => {
    return (
        <div>
            <NavLink to="/demography/add">Add</NavLink>
            <NavLink to="/demography/search">Search</NavLink>

        </div>
    );
};

export default Demography;