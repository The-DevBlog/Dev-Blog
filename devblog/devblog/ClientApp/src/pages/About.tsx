import "./styles/About.css";
import img from "../imgs/me.png";

const About = () => {
    return (
        <section className="about">
            <ul>
                <li><a target="_blank" rel="noreferrer" href="https://github.com/AndrewCS149">GitHub</a></li>
                <li><a target="_blank" rel="noreferrer" href="https://mastodon.social/@TheDevBlog">Mastodon</a></li>
                <li><a target="_blank" rel="noreferrer" href="https://andrew149.itch.io/">Itch.io</a></li>
                <li><a target="_blank" rel="noreferrer" href="https://discord.com/channels/1015686540460040405/1015686540460040408">Discord</a></li>
                <li><a target="_blank" rel="noreferrer" href="https://www.linkedin.com/in/andrew149/">LinkedIn</a></li>
            </ul>

            <img src={img} alt="The DevBlog creator Andrew Smith" />
        </section>
    )
}

export default About;