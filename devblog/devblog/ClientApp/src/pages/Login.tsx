const Login = () => {
    return (
        <div>
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
            </form>
        </div>
    )
}

export default Login;