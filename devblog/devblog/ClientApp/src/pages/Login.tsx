import "./Login.css";

const Login = () => {
    return (
        <div className="login">
            <form>
                <label>UserName</label>
                <input
                    type="text"
                    required
                />

                <label>Password</label>
                <input
                    type="password"
                    required
                />

                <button>Login</button>
            </form>
        </div>
    )
}

export default Login;